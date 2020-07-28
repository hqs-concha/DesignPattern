using DesignPattern.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.Controllers
{
    [ApiController]
    [Route("singleton")]
    public class SingletonController : ControllerBase
    {
        public IActionResult Index([FromServices] ISingletonService service)
        {
            return Ok(service.Get());
        }
    }
}
