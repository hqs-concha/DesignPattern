using System;
using System.Text.Json;
using DesignPattern.Interface.Event;
using DesignPattern.Model;

namespace DesignPattern.Services.Event
{
    public class OrderSubmittedHandler : IEventHandler<OrderSubmittedEvent>
    {
        public void Handler(OrderSubmittedEvent data)
        {
            Console.WriteLine("我进来了！");
            Console.WriteLine($"data:{JsonSerializer.Serialize(data)}");
        }
    }
}
