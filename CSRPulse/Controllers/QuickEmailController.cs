using CSRPulse.Model;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class QuickEmailController : BaseController<QuickEmailController>
    {
        private readonly IDropdownBindService _dropdownBindService;
        private readonly IQuickEmailService _quickEmailService;
        public QuickEmailController(IDropdownBindService dropdownBindService, IQuickEmailService quickEmailService)
        {
            _dropdownBindService = dropdownBindService;
            _quickEmailService = quickEmailService;
        }
        public IActionResult Index()
        {
            QuickEmail quickEmail = new QuickEmail();
            return View(new QuickEmail());
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(QuickEmail quickEmail)
        {
            bool Issuccess = false;
            try
            {
                if (quickEmail.ToEmails.Length == 0 && quickEmail.BccEmails.Length == 0)
                {
                    ModelState.AddModelError("", "Please specify at least one recipient");
                }
                if (ModelState.IsValid) { 
                    Issuccess = await _quickEmailService.SendEmailAsync(quickEmail);
                    if(Issuccess)
                        TempData["Message"] = "Email sending failed.";
                    else 
                        TempData["Error"] = "Failed to send your message. Please try later.";

                }
                else
                    return View(quickEmail);

            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to send your message Please contact the administrator.";
                return View(quickEmail);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
