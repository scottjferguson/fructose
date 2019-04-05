using Customer.Microservice.DTO;
using MediatR;

namespace Customer.Microservice.Command
{
    public class GetCustomerByIDCommand : IRequest<CustomerDTO>
    {
        public long ID { get; }

        #region ctor

        public GetCustomerByIDCommand(long id)
        {
            ID = id;
        }

        #endregion
    }
}
