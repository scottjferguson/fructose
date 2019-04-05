using Autofac;
using Fructose.Common.EventBus;
using Fructose.Common.EventBus.Impl;
using Fructose.Common.Exceptions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Fructose.Common.EventBus.Impl.InMemoryEventBusSubscriptionsManager;

namespace Fructose.EventBus.AzureServiceBus.Impl
{
    public class EventBusAzureServiceBus : IEventBus
    {
        private readonly IServiceBusPersisterConnection _serviceBusPersisterConnection;
        private readonly ILogger<EventBusAzureServiceBus> _logger;
        private readonly IEventBusSubscriptionsManager _subscriptionManager;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly SubscriptionClient _subscriptionClient;

        public EventBusAzureServiceBus(
            IServiceBusPersisterConnection serviceBusPersisterConnection,
            ILogger<EventBusAzureServiceBus> logger,
            IEventBusSubscriptionsManager subscriptionManager,
            string subscriptionClientName,
            ILifetimeScope lifetimeScope)
        {
            _serviceBusPersisterConnection = serviceBusPersisterConnection;
            _logger = logger;
            _subscriptionManager = subscriptionManager ?? new InMemoryEventBusSubscriptionsManager();
            _subscriptionClient = new SubscriptionClient(serviceBusPersisterConnection.ServiceBusConnectionStringBuilder, subscriptionClientName);
            _lifetimeScope = lifetimeScope;

            RemoveDefaultRule();
            RegisterSubscriptionClientMessageHandler();
        }

        public void Publish(IntegrationEvent integrationEvent)
        {
            string eventName = integrationEvent.GetType().Name.Replace(INTEGRATION_EVENT_SUFIX, String.Empty);
            string jsonMessage = JsonConvert.SerializeObject(integrationEvent);
            byte[] body = Encoding.UTF8.GetBytes(jsonMessage);

            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = body,
                Label = eventName,
            };

            ITopicClient topicClient = _serviceBusPersisterConnection.CreateModel();

            topicClient.SendAsync(message)
                .GetAwaiter()
                .GetResult();
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            string eventName = typeof(T).Name.Replace(INTEGRATION_EVENT_SUFIX, String.Empty);

            bool isContainsKeys = _subscriptionManager.HasSubscriptionsForEvent<T>();

            if (!isContainsKeys)
            {
                try
                {
                    _subscriptionClient.AddRuleAsync(
                        new RuleDescription
                        {
                            Name = eventName,
                            Filter = new CorrelationFilter
                            {
                                Label = eventName
                            }
                        }).GetAwaiter().GetResult();
                }
                catch (ServiceBusException)
                {
                    _logger.LogInformation($"The messaging entity {eventName} already exists.");
                }
            }

            _subscriptionManager.AddSubscription<T, TH>();
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IIntegrationEventDynamicHandler
        {
            _subscriptionManager.AddDynamicSubscription<TH>(eventName);
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            string eventName = typeof(T).Name.Replace(INTEGRATION_EVENT_SUFIX, String.Empty);

            try
            {
                _subscriptionClient
                     .RemoveRuleAsync(eventName)
                     .GetAwaiter()
                     .GetResult();
            }
            catch (MessagingEntityNotFoundException)
            {
                _logger.LogInformation($"The messaging entity {eventName} Could not be found.");
            }

            _subscriptionManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IIntegrationEventDynamicHandler
        {
            _subscriptionManager.RemoveDynamicSubscription<TH>(eventName);
        }

        public void Dispose()
        {
            _subscriptionManager.Clear();
        }

        #region Private

        private void RegisterSubscriptionClientMessageHandler()
        {
            _subscriptionClient.RegisterMessageHandler(
                async (message, token) =>
                {
                    string eventName = $"{message.Label}{INTEGRATION_EVENT_SUFIX}";
                    string messageData = Encoding.UTF8.GetString(message.Body);

                    // Complete the message so that it is not received again.
                    if (await ProcessEvent(eventName, messageData))
                    {
                        await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                    }
                },
                new MessageHandlerOptions(ExceptionReceivedHandler)
                {
                    MaxConcurrentCalls = MAX_CONCURRENT_CALLS,
                    AutoComplete = false
                });
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            ExceptionReceivedContext context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            string message = String.Join(Environment.NewLine,
                $"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.",
                $"- Endpoint         : {context.Endpoint}",
                $"- Entity Path      : {context.EntityPath}",
                $"- Executing Action : {context.Action}");

            _logger.LogError(new MicroserviceException(exceptionReceivedEventArgs.Exception, message), "ExceptionReceivedHandler was invoked");

            return Task.CompletedTask;
        }

        private async Task<bool> ProcessEvent(string eventName, string message)
        {
            var processed = false;

            if (_subscriptionManager.HasSubscriptionsForEvent(eventName))
            {
                using (ILifetimeScope scope = _lifetimeScope.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    IEnumerable<SubscriptionInfo> subscriptions = _subscriptionManager.GetHandlersForEvent(eventName);

                    foreach (SubscriptionInfo subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType) as IIntegrationEventDynamicHandler;

                            if (handler == null)
                            {
                                continue;
                            }

                            dynamic eventData = JObject.Parse(message);

                            await handler.Handle(eventData);
                        }
                        else
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType);

                            if (handler == null)
                            {
                                continue;
                            }

                            Type eventType = _subscriptionManager.GetEventTypeByName(eventName);

                            object integrationEvent = JsonConvert.DeserializeObject(message, eventType);

                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                        }
                    }
                }

                processed = true;
            }

            return processed;
        }

        private void RemoveDefaultRule()
        {
            try
            {
                _subscriptionClient
                     .RemoveRuleAsync(RuleDescription.DefaultRuleName)
                     .GetAwaiter()
                     .GetResult();
            }
            catch (MessagingEntityNotFoundException)
            {
                _logger.LogInformation($"The messaging entity { RuleDescription.DefaultRuleName } Could not be found.");
            }
        }

        #endregion

        #region Settings

        private const string AUTOFAC_SCOPE_NAME = "fructose_event_bus";
        private const string INTEGRATION_EVENT_SUFIX = "IntegrationEvent";
        private const int MAX_CONCURRENT_CALLS = 10;

        #endregion
    }
}
