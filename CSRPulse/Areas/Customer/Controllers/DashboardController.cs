using CSRPulse.Controllers;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("Customer/[Controller]/[action]")]
    public class DashboardController : BaseController<DashboardController>
    {

        private readonly IAccountService _accountService;
        public DashboardController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetActiveUser()
        {
            var getActiveUser = _accountService.GetUserAsync();
            return PartialView("_ActiveUser", getActiveUser);
        }
    }
}
