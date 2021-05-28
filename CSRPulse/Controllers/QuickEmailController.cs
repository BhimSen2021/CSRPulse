using CSRPulse.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class QuickEmailController : BaseController<QuickEmailController>
    {
        public QuickEmailController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> QuickEmail()
        {
            return await Task.FromResult((IActionResult)PartialView("_QuickEmail", new QuickEmail()));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult QuickEmail(QuickEmail quickEmail)
        {
            bool Issuccess = false;
            try
            {
                Issuccess = true;
            }
            catch (Exception)
            {
                Issuccess = false;
                throw;
            }
            return Json(new { success = Issuccess, htmlData = ConvertViewToString("_QuickEmail", quickEmail, true) });
        }
    }
}
