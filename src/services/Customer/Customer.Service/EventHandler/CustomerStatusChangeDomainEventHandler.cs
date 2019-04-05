using Core.Data;
using Core.Exceptions;
using Core.Framework;
using Core.Validation;
using Customer.Domain.Repository;
using Customer.Microservice.Event;
using Customer.Microservice.EventHandler.Base;
using Fructose.Common.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Microservice.EventHandler
{
    public class CustomerStatusChangeDomainEventHandler : EventHandlerBase, INotificationHandler<CustomerStatusChangeDomainEvent>
    {
        private readonly Lazy<IAbstractRepositoryFactory> _abstractRepositoryFactory;
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly Lazy<IValidatorInline> _validator;
        private readonly Lazy<ILookupDataRepository> _lookupDataRepository;

        public CustomerStatusChangeDomainEventHandler(
            Lazy<IAbstractRepositoryFactory> abstractRepositoryFactory,
            Lazy<IUnitOfWork> unitOfWork,
            Lazy<IValidatorInline> validator,
            Lazy<ILookupDataRepository> lookupDataRepository)
        {
            _abstractRepositoryFactory = abstractRepositoryFactory;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _lookupDataRepository = lookupDataRepository;
        }

        public async Task Handle(CustomerStatusChangeDomainEvent notification, CancellationToken cancellationToken)
        {
            _validator.Value
                .NotInvalidID(() => notification.CustomerId)
                .NotNullOrEmpty(() => notification.CustomerStatusCode)
                .Validate();

            IRepository<Domain.Entity.Customer> repository = _abstractRepositoryFactory.Value.Create(FructoseRepository)
                .Create<Domain.Entity.Customer>();

            Domain.Entity.Customer customer = repository.Query()
                .SingleOrDefault(c => c.Id == notification.CustomerId);

            if (customer == null)
            {
                throw new MicroserviceException(ErrorCode.NODA, $"Could not find a Customer with ID = ${notification.CustomerId}");
            }

            LookupData customerStatus = _lookupDataRepository.Value.GetAllCustomerStatuses()
                .SingleOrDefault(cs => String.Equals(cs.Code, notification.CustomerStatusCode, StringComparison.OrdinalIgnoreCase));

            if (customerStatus == null)
            {
                throw new MicroserviceException(ErrorCode.NODA, $"Could not find a Customer Status with Code = ${notification.CustomerStatusCode}");
            }

            customer.CustomerStatusId = customerStatus.Id;

            await _unitOfWork.Value.CommitAsync();
        }
    }
}
