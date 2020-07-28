using System.Threading.Tasks;
using DesignPattern.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.Controllers
{
    /// <summary>
    /// 拦截器
    /// https://autofaccn.readthedocs.io/zh/latest/advanced/interceptors.html
    /// </summary>
    [ApiController]
    [Route("interceptor")]
    public class InterceptorController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public InterceptorController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index([FromQuery] int id)
        {
            var hasCache = _customerService.Get(id);
            var noCache = _customerService.NoAspect();
            var asyncMethod = await _customerService.GetAsync(id);
            return Ok(new {hasCache, noCache, asyncMethod});
        }
    }
}
