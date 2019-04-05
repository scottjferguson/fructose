using MediatR;

namespace Customer.Microservice.Command
{
    public class DeleteCustomerCommand : IRequest
    {
        public long ID { get; }

        #region ctor

        public DeleteCustomerCommand(long id)
        {
            ID = id;
        }

        #endregion
    }
}
