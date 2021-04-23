using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Services.IServices;
namespace CSRPulse.Controllers
{
    public class RegistrationController : BaseController<RegistrationController>
    {
        private readonly IRegistrationService registrationService;
        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;

        }
        public IActionResult Index()
        {
            _logger.LogInformation("RegistrationController/Index");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            return await Task.FromResult((IActionResult)PartialView("_Registration", new Model.Customer()));
        }


        /// <summary>
        /// To Register New Cutomer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Registration(Model.Customer customer)
        {
            _logger.LogInformation("RegistrationController/Index");
            try
            {
                if (!customer.IsAgree)
                    ModelState.AddModelError("IsAgree", "Please select terms & condition");

                if (ModelState.IsValid)
                {
                    customer.CreatedBy = 1;
                    string customerCode = await registrationService.CustomerRegistrationAsync(customer);
                    if (!string.IsNullOrEmpty(customerCode))
                    {
                        customer.CustomerCode = customerCode;
                        return Json(new { htmlData = ConvertViewToString("_PaymentOption", customer, true) });
                    }
                }
                return Json(new { htmlData = ConvertViewToString("_Registration", customer, true) });

            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> PaymentOption(Model.Customer customer, string ButtonType)
        {
            try
            {

                if (ButtonType == "free")
                {
                    return Json(new { success=1 });
                }
                return new ContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

    }
}
