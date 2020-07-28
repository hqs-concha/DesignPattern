using DesignPattern.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        public IActionResult Index([FromServices] IOrderService service)
        {
            return Ok();
        }
    }
}
