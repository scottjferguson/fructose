using Fructose.Common.EventBus;
using System.Threading.Tasks;

namespace Customer.Microservice.Integration
{
    public interface ICustomerIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync();
        Task AddAndSaveEventAsync(IntegrationEvent integrationEvent);
    }
}
