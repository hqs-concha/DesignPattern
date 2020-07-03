using DesignPattern.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.Controllers
{
    /// <summary>
    /// ChangeToken 的使用
    /// https://www.cnblogs.com/jionsoft/p/12249326.html
    /// https://www.bookstack.cn/read/asp/921cd89ff2fc84b7.md
    /// </summary>
    [ApiController]
    [Route("change-token")]
    public class ChangeTokenController : ControllerBase
    {
        private readonly ICustomMonitor _monitor;

        public ChangeTokenController(ICustomMonitor monitor)
        {
            _monitor = monitor;
        }

        public IActionResult Index()
        {
            _monitor.MonitoringEnabled = true;
            _monitor.CurrentState = "Monitoring!";
            _monitor.OnChange();
            return Ok();
        }
    }
}
