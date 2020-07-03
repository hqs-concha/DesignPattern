
using System;
using DesignPattern.Interface.Event;
using DesignPattern.Model;
using DesignPattern.Services.Event;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.Controllers
{
    /// <summary>
    /// 进程内的事件总线
    /// https://www.cnblogs.com/lwqlun/p/10468058.html
    /// </summary>
    [ApiController]
    [Route("event-bus")]
    public class EventBusController : ControllerBase
    {
        private readonly IEventBus _bus;

        public EventBusController(IEventBus bus)
        {
            _bus = bus;
        }

        public IActionResult Index()
        {
            _bus.PublishAsync(new OrderSubmittedEvent
            {
                OrderNo = new Random().Next(10000,99999999),
                Remarks = "ha ha ha"
            });

            return Ok("Publish successfully");
        }

        [HttpGet("subscribe")]
        public IActionResult Subscribe()
        {
            _bus.Subscribe<OrderSubmittedEvent, OrderSubmittedHandler>();
            return Ok("Subscribe successfully");
        }

        [HttpGet("unsubscribe")]
        public IActionResult Unsubscribe()
        {
            _bus.Unsubscribe<OrderSubmittedEvent, OrderSubmittedHandler>();
            return Ok("Unsubscribe successfully");
        }
    }
}
