using Customer.Microservice.DTO;
using MediatR;

namespace Customer.Microservice.Event
{
    public class CustomerEnrollmentDomainEvent : INotification
    {
        public CustomerDTO Customer { get; }

        public CustomerEnrollmentDomainEvent(CustomerDTO customer)
        {
            Customer = customer;
        }
    }
}
