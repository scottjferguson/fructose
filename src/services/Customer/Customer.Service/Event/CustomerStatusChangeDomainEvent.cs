using MediatR;

namespace Customer.Microservice.Event
{
    public class CustomerStatusChangeDomainEvent : INotification
    {
        public long CustomerId { get; }
        public string CustomerStatusCode { get; }

        public CustomerStatusChangeDomainEvent(long customerId, string customerStatusCode)
        {
            CustomerId = customerId;
            CustomerStatusCode = customerStatusCode;
        }
    }
}
