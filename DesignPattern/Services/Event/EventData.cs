using System;
using DesignPattern.Interface.Event;

namespace DesignPattern.Services.Event
{
    public class EventData : IEvent
    {
        public Guid Id { get; set; }
        public long TimeStamp { get; set; }

        public EventData()
        {
            Id = Guid.NewGuid();
            TimeStamp = DateTime.Now.Ticks;
        }
    }
}
