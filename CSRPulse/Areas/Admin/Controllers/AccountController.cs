using CSRPulse.Model;
using CSRPulse.Services;
using DNTCaptcha.Core;
using DNTCaptcha.Core.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class AccountController :  Controller
    {
        private Guid userSessionId;
        private readonly IAccountService _accountService;
        private readonly ILogger<HomeController> _logger;
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly IMemoryCache memoryCache;
        public AccountController(IAccountService accountService, IDNTCaptchaValidatorService validatorService, IMemoryCache memoryCache, ILogger<HomeController> logger)
        {
            _accountService = accountService;           
            _validatorService = validatorService;
            this.memoryCache = memoryCache;
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(SingIn singIn, string returnUrl)
        {
           
            _logger.LogInformation("Admin/AccountController/Login");
            try
            {
                if (string.IsNullOrEmpty(singIn.hdUserName))
                    ModelState.AddModelError("UserName", "Please enter user name.");
                if (string.IsNullOrEmpty(singIn.hdPassword))
                    ModelState.AddModelError("Password", "Please enter password.");

                if (ModelState.IsValid)
                {
                    if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                    {

                        this.ModelState.AddModelError(DNTCaptchaTagHelper.CaptchaInputName, "Please Enter Valid Captcha.");
                        return View(singIn);
                    }

                    singIn.UserName = Password.DecryptStringAES(singIn.hdUserName);
                    singIn.Password = Password.DecryptStringAES(singIn.hdPassword);


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
                       
                        bool isExist = memoryCache.TryGetValue(uDetail.UserID, out userSessionId);
                        if (!isExist)
                        {
                            userSessionId = GetUserSessionId();                            
                            memoryCache.Set(uDetail.UserID, userSessionId);
                        }
                        else
                        {
                            TempData["logoutAlert"] = "The previous session was not logged out. If you do wish to login now , please press the logout button to close the previous session, else press cancel.";

                            ModelState.AddModelError("", "The previous session was not logged out.");
                            return View(singIn);
                        }
                     

                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    else
                    {
                        if (uDetail.ErrorMessage == "notexists")
                        {
                            ModelState.AddModelError("", "Please enter correct user name.");
                        }
                        else if (uDetail.WrongAttemp.HasValue)
                        {
                            if (uDetail.WrongAttemp.Value == 0)
                                ModelState.AddModelError("WrongAttemp", "Your account is temporary locked, please contact your administrator.");
                            else
                            {
                                ModelState.AddModelError("WrongAttemp", $"{uDetail.WrongAttemp.Value} attemp(s) left.");
                                ModelState.AddModelError("", "Invalid credentials");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("WrongAttemp", $"Your account is temporary locked, please try after {uDetail.ErrorMessage}");
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
                bool isExist = memoryCache.TryGetValue(userDetail.UserID, out userSessionId);
                if (isExist)
                {
                    memoryCache.Remove(userDetail.UserID);
                }
            }
            return View();
        }

        private Guid GetUserSessionId()
        {
            return Guid.NewGuid();
        }
    }
}
