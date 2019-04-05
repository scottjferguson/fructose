using System;
using System.Data.Common;
using System.Threading.Tasks;
using Customer.Infrastructure.ORM.EntityFramework;
using Fructose.Common.EventBus;
using Fructose.EventLog.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Customer.Microservice.Integration.Impl
{
    public class CustomerIntegrationEventService : ICustomerIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        private readonly FructoseContext _fructoseContext;
        private readonly IIntegrationEventLogService _eventLogService;

        public CustomerIntegrationEventService(
            IEventBus eventBus,
            FructoseContext fructoseContext,
            IntegrationEventLogContext eventLogContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _eventBus = eventBus;
            _fructoseContext = fructoseContext;
            _eventLogService = integrationEventLogServiceFactory(fructoseContext.Database.GetDbConnection());
        }

        public async Task PublishEventsThroughEventBusAsync()
        {
            var pendindLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync();

            foreach (IntegrationEventLogEntry logEvent in pendindLogEvents)
            {
                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvent.EventId);

                    _eventBus.Publish(logEvent.IntegrationEvent);

                    await _eventLogService.MarkEventAsPublishedAsync(logEvent.EventId);
                }
                catch (Exception)
                {
                    await _eventLogService.MarkEventAsFailedAsync(logEvent.EventId);
                }
            }
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent integrationEvent)
        {
            // TODO: SF: Pass the DbTransaction from IUnitOfWork ala OrderingContext
            await _eventLogService.SaveEventAsync(integrationEvent, null);
        }
    }
}
