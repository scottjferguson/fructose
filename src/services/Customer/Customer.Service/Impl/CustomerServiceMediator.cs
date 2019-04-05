using Core.Providers;
using Customer.Microservice.Command;
using Customer.Microservice.DTO;
using MediatR;
using System.Threading.Tasks;

namespace Customer.Microservice.Impl
{
    public class CustomerServiceMediator : ICustomerService
    {
        private readonly IMediator _mediator;
        private readonly ICancellationTokenProvider _cancellationTokenProvider;

        public CustomerServiceMediator(IMediator mediator, ICancellationTokenProvider cancellationTokenProvider)
        {
            _mediator = mediator;
            _cancellationTokenProvider = cancellationTokenProvider;
        }

        public long Create(CustomerDTO customerDTO)
        {
            return _mediator.Send(new CreateCustomerCommand(customerDTO), _cancellationTokenProvider.Get()).Result;
        }

        public async Task<long> CreateAsync(CustomerDTO customerDTO)
        {
            return await _mediator.Send(new CreateCustomerCommand(customerDTO), _cancellationTokenProvider.Get());
        }

        public CustomerDTO GetByID(long id)
        {
            return _mediator.Send(new GetCustomerByIDCommand(id), _cancellationTokenProvider.Get()).Result;
        }

        public async Task<CustomerDTO> GetByIDAsync(long id)
        {
            return await _mediator.Send(new GetCustomerByIDCommand(id), _cancellationTokenProvider.Get());
        }

        public void Update(CustomerDTO customerDTO)
        {
            _mediator.Send(new UpdateCustomerCommand(customerDTO), _cancellationTokenProvider.Get());
        }

        public async Task UpdateAsync(CustomerDTO customerDTO)
        {
            await _mediator.Send(new UpdateCustomerCommand(customerDTO), _cancellationTokenProvider.Get());
        }

        public void Delete(long id)
        {
            _mediator.Send(new DeleteCustomerCommand(id), _cancellationTokenProvider.Get());
        }

        public async Task DeleteAsync(long id)
        {
            await _mediator.Send(new DeleteCustomerCommand(id), _cancellationTokenProvider.Get());
        }
    }
}
