using Customer.Microservice.DTO;
using MediatR;

namespace Customer.Microservice.Command
{
    public class CreateCustomerCommand : IRequest<long>
    {
        public CustomerDTO Customer { get; }

        #region ctor

        public CreateCustomerCommand(CustomerDTO customer)
        {
            Customer = customer;
        }

        #endregion
    }
}
