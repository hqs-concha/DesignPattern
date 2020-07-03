
namespace DesignPattern.Interface.Event
{
    /// <summary>
    /// 空接口，用来标记
    /// </summary>
    public interface IEventHandler
    {

    }

    
    public interface IEventHandler<TEvent> : IEventHandler where TEvent : IEvent
    {
        void Handler(TEvent data);
    }
}
