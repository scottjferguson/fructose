using Core.Data;
using Core.Mapping;
using Core.Validation;
using Customer.Microservice.CommandHandler.Base;
using Customer.Microservice.Command;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Microservice.CommandHandler
{
    public class CreateCustomerCommandHandler : CommandHandlerBase, IRequestHandler<CreateCustomerCommand, long>
    {
        private readonly Lazy<IAbstractRepositoryFactory> _abstractRepositoryFactory;
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly Lazy<IValidatorInline> _validator;
        private readonly Lazy<IMapper> _mapper;

        public CreateCustomerCommandHandler(
            Lazy<IAbstractRepositoryFactory> abstractRepositoryFactory,
            Lazy<IUnitOfWork> unitOfWork,
            Lazy<IValidatorInline> validator,
            Lazy<IMapper> mapper)
        {
            _abstractRepositoryFactory = abstractRepositoryFactory;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<long> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
             _validator.Value
                .NotNull(() => request)
                .NotNull(() => request.Customer)
                .Validate();

            Domain.Entity.Customer customer = _mapper.Value.MapTo<Domain.Entity.Customer>(request.Customer);

            IRepository<Domain.Entity.Customer> repository = _abstractRepositoryFactory.Value.Create(FructoseRepository)
                .Create<Domain.Entity.Customer>();

            repository.Add(customer);

            await _unitOfWork.Value.CommitAsync();

            return customer.Id;
        }
    }
}
