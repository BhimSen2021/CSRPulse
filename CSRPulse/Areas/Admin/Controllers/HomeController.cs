using CSRPulse.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class HomeController : BaseController<CustomerController>
    {
        public IActionResult Index()
        {
            _logger.LogInformation("Admin/Home/Index");
            return View();
        }
    }
}
