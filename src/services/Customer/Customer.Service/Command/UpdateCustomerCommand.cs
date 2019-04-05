using Customer.Microservice.DTO;
using MediatR;

namespace Customer.Microservice.Command
{
    public class UpdateCustomerCommand : IRequest
    {
        public CustomerDTO Customer { get; }

        #region ctor

        public UpdateCustomerCommand(CustomerDTO customer)
        {
            Customer = customer;
        }

        #endregion
    }
}
