using Core.Data;
using Core.Mapping;
using Core.Validation;
using Customer.Microservice.CommandHandler.Base;
using Customer.Microservice.Command;
using Customer.Microservice.DTO;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Microservice.CommandHandler
{
    public class GetCustomerByIDCommandHandler : CommandHandlerBase, IRequestHandler<GetCustomerByIDCommand, CustomerDTO>
    {
        private readonly Lazy<IAbstractRepositoryFactory> _abstractRepositoryFactory;
        private readonly Lazy<IValidatorInline> _validator;
        private readonly Lazy<IMapper> _mapper;

        public GetCustomerByIDCommandHandler(
            Lazy<IAbstractRepositoryFactory> abstractRepositoryFactory,
            Lazy<IValidatorInline> validator,
            Lazy<IMapper> mapper)
        {
            _abstractRepositoryFactory = abstractRepositoryFactory;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> Handle(GetCustomerByIDCommand request, CancellationToken cancellationToken)
        {
            _validator.Value
                .NotNull(() => request)
                .NotInvalidID(() => request.ID)
                .Validate();

            IRepository<Domain.Entity.Customer> repository = _abstractRepositoryFactory.Value.Create(FructoseRepository)
                .Create<Domain.Entity.Customer>();

            Domain.Entity.Customer customer = repository.Query()
                .SingleOrDefault(c => c.Id == request.ID);

            CustomerDTO customerDTO = _mapper.Value.MapTo<CustomerDTO>(customer);

            return customerDTO;
        }
    }
}
