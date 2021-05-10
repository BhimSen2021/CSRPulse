using CSRPulse.Controllers;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class CustomerController : BaseController<PlanController>
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<IActionResult> CustomerListAsync()
        {
            _logger.LogInformation("Admin/Customer/CustomerList");
            try
            {
                var customerList = await _customerService.GetAllCustomerAsync();
                return View(customerList);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }
    }
}
