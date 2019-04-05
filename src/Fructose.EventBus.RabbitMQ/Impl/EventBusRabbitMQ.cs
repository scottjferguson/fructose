
using Autofac;
using Fructose.Common.EventBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static Fructose.Common.EventBus.Impl.InMemoryEventBusSubscriptionsManager;

namespace Fructose.EventBus.RabbitMQ.Impl
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    { 
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IEventBusSubscriptionsManager _subscriptionManager;

        private readonly int _retryCount;
        private IModel _consumerChannel;
        private string _queueName;

        public EventBusRabbitMQ(
            IRabbitMQPersistentConnection persistentConnection,
            ILogger<EventBusRabbitMQ> logger,
            ILifetimeScope lifetimeScope,
            IEventBusSubscriptionsManager subscriptionManager,
            string queueName = null,
            int retryCount = 5)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
            _lifetimeScope = lifetimeScope;
            _subscriptionManager = subscriptionManager;
            _queueName = queueName;
            _retryCount = retryCount;
            _consumerChannel = CreateConsumerChannel();
            _subscriptionManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        public void Publish(IntegrationEvent integrationEvent)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            RetryPolicy policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex.ToString());
                });

            using (IModel channel = _persistentConnection.CreateModel())
            {
                string eventName = integrationEvent.GetType().Name;

                channel.ExchangeDeclare(exchange: BROKER_NAME, type: CHANNEL_TYPE_DIRECT);

                string message = JsonConvert.SerializeObject(integrationEvent);
                byte[] body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    IBasicProperties properties = channel.CreateBasicProperties();

                    properties.DeliveryMode = DELIVERY_MODE_PERSISTENT;

                    channel.BasicPublish(exchange: BROKER_NAME, routingKey: eventName, mandatory: true, basicProperties: properties, body: body);
                });
            }
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            string eventName = _subscriptionManager.GetEventKey<T>();

            DoInternalSubscription(eventName);

            _subscriptionManager.AddSubscription<T, TH>();
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IIntegrationEventDynamicHandler
        {
            DoInternalSubscription(eventName);

            _subscriptionManager.AddDynamicSubscription<TH>(eventName);
        }

        public void Unsubscribe<T, TH>() where TH : IIntegrationEventHandler<T> where T : IntegrationEvent
        {
            _subscriptionManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IIntegrationEventDynamicHandler
        {
            _subscriptionManager.RemoveDynamicSubscription<TH>(eventName);
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }

            _subscriptionManager.Clear();
        }

        #region Private

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueUnbind(queue: _queueName, exchange: BROKER_NAME, routingKey: eventName);

                if (_subscriptionManager.IsEmpty)
                {
                    _queueName = string.Empty;
                    _consumerChannel.Close();
                }
            }
        }

        private void DoInternalSubscription(string eventName)
        {
            bool isContainsKey = _subscriptionManager.HasSubscriptionsForEvent(eventName);

            if (!isContainsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using (IModel channel = _persistentConnection.CreateModel())
                {
                    channel.QueueBind(queue: _queueName, exchange: BROKER_NAME, routingKey: eventName);
                }
            }
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME, type: CHANNEL_TYPE_DIRECT);

            channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body);

                await ProcessEvent(eventName, message);

                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };

            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
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
            }
        }

        #endregion

        #region Settings

        private const string AUTOFAC_SCOPE_NAME = "fructose_event_bus";
        private const string BROKER_NAME = "fructose_event_bus";
        private const string CHANNEL_TYPE_DIRECT = "direct";
        private const int DELIVERY_MODE_PERSISTENT = 2;

        #endregion
    }
}
