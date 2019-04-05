using System.Threading.Tasks;

namespace Fructose.Common.EventBus
{
    public interface IIntegrationEventDynamicHandler
    {
        Task Handle(dynamic eventData);
    }
}
