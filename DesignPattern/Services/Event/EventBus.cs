
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DesignPattern.Interface.Event;

namespace DesignPattern.Services.Event
{
    /// <summary>
    /// 时间总线
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly Dictionary<string, List<Type>> Handlers = new Dictionary<string, List<Type>>();

        public EventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Subscribe<TEvent, THandler>() where TEvent : IEvent where THandler : IEventHandler<TEvent>
        {
            var name = typeof(TEvent).Name;

            if (!Handlers.ContainsKey(name))
            {
                Handlers.Add(name, new List<Type> { });
            }

            Handlers[name].Add(typeof(THandler));
        }

        public void Unsubscribe<TEvent, THandler>() where TEvent : IEvent where THandler : IEventHandler<TEvent>
        {
            var name = typeof(TEvent).Name;

            if(!Handlers.ContainsKey(name)) return;

            Handlers[name].Remove(typeof(THandler));

            if (Handlers[name].Count == 0)
            {
                Handlers.Remove(name);
            }
        }

        public void Publish<TEvent>(TEvent o) where TEvent : IEvent
        {
            var name = typeof(TEvent).Name;
            
            if (Handlers.ContainsKey(name))
            {
                foreach (var handler in Handlers[name])
                {
                    var service = (IEventHandler<TEvent>)_serviceProvider.GetService(handler);
                    service.Handler(o);
                }
            }
        }

        public Task PublishAsync<TEvent>(TEvent o) where TEvent : IEvent
        {
            Task.Run(() => Publish(o));
            return Task.CompletedTask;
        }
    }
}
