using Customer.API.IntegrationEvents.Events;
using Customer.Microservice;
using Fructose.Common.EventBus;
using System.Threading.Tasks;

namespace Customer.API.IntegrationEvents.EventHandling
{
    public class CustomerEnrollmentIntegrationEventHandler : IIntegrationEventHandler<CustomerEnrollmentIntegrationEvent>
    {
        private readonly ICustomerService _customerService;

        public CustomerEnrollmentIntegrationEventHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task Handle(CustomerEnrollmentIntegrationEvent integrationEvent)
        {
            await _customerService.CreateAsync(integrationEvent.Customer);
        }
    }
}



