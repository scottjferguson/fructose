namespace Fructose.Common.EventBus
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent integrationEvent);
        void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
        void SubscribeDynamic<TH>(string eventName) where TH : IIntegrationEventDynamicHandler;
        void Unsubscribe<T, TH>() where TH : IIntegrationEventHandler<T> where T : IntegrationEvent;
        void UnsubscribeDynamic<TH>(string eventName) where TH : IIntegrationEventDynamicHandler;
    }
}
