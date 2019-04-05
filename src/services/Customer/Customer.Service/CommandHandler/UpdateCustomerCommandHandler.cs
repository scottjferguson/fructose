using Core.Data;
using Core.Mapping;
using Core.Validation;
using Customer.Microservice.CommandHandler.Base;
using Customer.Microservice.Command;
using Fructose.Common.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Exceptions;

namespace Customer.Microservice.CommandHandler
{
    public class UpdateCustomerCommandHandler : CommandHandlerBase, IRequestHandler<UpdateCustomerCommand>
    {
        private readonly Lazy<IAbstractRepositoryFactory> _abstractRepositoryFactory;
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly Lazy<IValidatorInline> _validator;
        private readonly Lazy<IMapper> _mapper;

        public UpdateCustomerCommandHandler(
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

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            _validator.Value
                .NotNull(() => request)
                .NotNull(() => request.Customer)
                .NotInvalidID(() => request.Customer.ID)
                .Validate();

            IRepository<Domain.Entity.Customer> repository = _abstractRepositoryFactory.Value.Create(FructoseRepository)
                .Create<Domain.Entity.Customer>();

            Domain.Entity.Customer customer = repository.Query()
                .SingleOrDefault(c => c.Id == request.Customer.ID);

            if (customer == null)
            {
                throw new MicroserviceException(ErrorCode.NODA, $"Could not find a Customer with ID = ${request.Customer.ID}");
            }

            _mapper.Value.MapOver(request.Customer, customer);

            await _unitOfWork.Value.CommitAsync();

            return Unit.Value;
        }
    }
}
