using CSRPulse.Model;
using CSRPulse.Services.IServices;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Customer")]
    [Route("Customer/[Controller]/[action]")]
    public class UserController : CSRPulse.Controllers.BaseController<UserController>
    {
        private readonly IRegistrationService _registrationService;
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDropdownBindService ddlService;

        public UserController(IRegistrationService registrationService, IAccountService accountService, IWebHostEnvironment webHostEnvironment, IDropdownBindService ddlService) : base()
        {
            _registrationService = registrationService;
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
            this.ddlService = ddlService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("Customer/RegistrationController/Index");
            try
            {
                var result =  _accountService.GetUserProfileAsync();
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            BindDropdowns();
            return View(new SignUp());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SignUp singUp)
        {
            try
            {
                _logger.LogInformation("Customer/RegistrationController/Create");
                BindDropdowns();
                if (ModelState.IsValid)
                {
                    if (singUp.ImagePhoto != null)
                    {
                        string folder = "assets/images/users/";
                        singUp.ImageName = await UploadImage(folder, singUp.ImagePhoto);
                    }
                    else
                        singUp.ImageName = "sample-profile.png";

                    var result = await _registrationService.RegistrationAsync(singUp);
                    if (singUp.RecordExist)
                    {
                        ModelState.AddModelError("", "User already exists");
                    }
                    if (result > 0)
                    {
                        TempData["Message"] = "User Registred Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(singUp);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        public JsonResult UserActiveDeActive(int userId, bool IsActive)
        {
            _logger.LogInformation("Customer/RegistrationController/UserActiveDeActive");
            var result = _registrationService.UserActiveDeActive(userId, IsActive);
            return Json(result);
        }
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, folderPath)))
                Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, folderPath));

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return folderPath;
        }

        [NonAction]
        void BindDropdowns()
        {
            var stateList = ddlService.GetRole(null);
            ViewBag.ddlRole = new SelectList(stateList, "id", "value");
        }

    }
}
