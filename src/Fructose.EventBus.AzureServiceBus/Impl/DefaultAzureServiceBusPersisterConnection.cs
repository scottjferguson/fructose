using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Fructose.EventBus.AzureServiceBus.Impl
{
    public class DefaultAzureServiceBusPersisterConnection : IServiceBusPersisterConnection
    {
        private readonly ILogger<DefaultAzureServiceBusPersisterConnection> _logger;
        private readonly ServiceBusConnectionStringBuilder _serviceBusConnectionStringBuilder;
        private ITopicClient _topicClient;

        public DefaultAzureServiceBusPersisterConnection(
            ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder,
            ILogger<DefaultAzureServiceBusPersisterConnection> logger)
        {
            _logger = logger;
            _serviceBusConnectionStringBuilder = serviceBusConnectionStringBuilder;
            _topicClient = new TopicClient(ServiceBusConnectionStringBuilder, RetryPolicy.Default);
        }

        public ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder => _serviceBusConnectionStringBuilder;

        public ITopicClient CreateModel()
        {
            if (_topicClient.IsClosedOrClosing)
            {
                _topicClient = new TopicClient(ServiceBusConnectionStringBuilder, RetryPolicy.Default);
            }

            return _topicClient;
        }

        bool _disposed;
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
        }
    }
}
