
using System.Threading.Tasks;

namespace DesignPattern.Interface.Event
{
    public interface IEventBus
    {
        void Subscribe<TEvent, THandler>() where TEvent : IEvent where THandler : IEventHandler<TEvent>;
        void Unsubscribe<TEvent, THandler>() where TEvent : IEvent where THandler : IEventHandler<TEvent>;
        void Publish<TEvent>(TEvent o) where TEvent : IEvent;
        Task PublishAsync<TEvent>(TEvent o) where TEvent : IEvent;
    }
}
