using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class RegistrationController : BaseController<RegistrationController>
    {
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
            if (ModelState.IsValid)
            {
               
            }
            //return new PartialViewResult
            //{
            //    ViewName = "_Registration",
            //    ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Model.Customer>(ViewData, customer)
            //};

           return await Task.FromResult((IActionResult)PartialView("_Registration", customer));
            //return Json(new { htmlData=ConvertViewToString("_Registration", customer,true) });
        }
    }
}
