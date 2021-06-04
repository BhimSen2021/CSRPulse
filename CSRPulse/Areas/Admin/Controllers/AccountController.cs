using CSRPulse.Controllers;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Model;
using DNTCaptcha.Core;
using DNTCaptcha.Core.Providers;
namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class AccountController : BaseController<AccountController>
    {
        private readonly IAccountService _accountService;
        private readonly IMenuService _menuService;
        private readonly IDNTCaptchaValidatorService _validatorService;
        public AccountController(IAccountService accountService, IMenuService menuService, IDNTCaptchaValidatorService validatorService)
        {
            _accountService = accountService;
            _menuService = menuService;
            _validatorService = validatorService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(SingIn singIn, string returnUrl)
        {
            _logger.LogInformation("Admin/AccountController/Login");
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                    {

                        this.ModelState.AddModelError(DNTCaptchaTagHelper.CaptchaInputName, "Please Enter Valid Captcha.");
                        return View(singIn);
                    }

                    bool isAuthenticated = false;
                    UserDetail uDetail = new UserDetail();
                    isAuthenticated = _accountService.AuthenticateUser(singIn, out uDetail);
                    if (isAuthenticated)
                    {
                        HttpContext.Session.SetComplexData("User", uDetail);

                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    else
                    {
                        if (uDetail.WrongAttemp.HasValue)
                        {
                            if (uDetail.WrongAttemp.Value == 0)
                                ModelState.AddModelError("WrongAttemp", "Your account is temperory locked, please contact your administrator.");
                            else
                            {
                                ModelState.AddModelError("WrongAttemp", $"{uDetail.WrongAttemp.Value} attemp(s) left.");
                                ModelState.AddModelError("", "Invalid credentials");
                            }
                        }

                    }

                }
                return View(singIn);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        [HttpGet]
        public IActionResult Logout()
        {
            _logger.LogInformation("Amin/AccountController/Logout");
            UserDetail userDetail = HttpContext.Session.GetComplexData<UserDetail>("User");
            if (userDetail != null)
            {
                HttpContext.Session.Clear();                
            }
            
            return RedirectToAction("Landing", "Home", new { Area = "" });
        }

        
    }
}
