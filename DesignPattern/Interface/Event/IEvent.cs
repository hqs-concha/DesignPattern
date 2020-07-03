using System;

namespace DesignPattern.Interface.Event
{
    public interface IEvent
    {
        Guid Id { get; set; } 
        long TimeStamp { get; set; }
    }
}
