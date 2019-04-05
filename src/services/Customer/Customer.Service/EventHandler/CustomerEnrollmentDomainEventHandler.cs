using Customer.Microservice.Command;
using Customer.Microservice.Event;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Microservice.EventHandler
{
    public class CustomerEnrollmentDomainEventHandler : INotificationHandler<CustomerEnrollmentDomainEvent>
    {
        private readonly IMediator _mediator;

        public CustomerEnrollmentDomainEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(CustomerEnrollmentDomainEvent notification, CancellationToken cancellationToken)
        {
            var createCustomerCommand = new CreateCustomerCommand(notification.Customer);

            await _mediator.Send(createCustomerCommand, cancellationToken);
        }
    }
}
