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
                var result = _accountService.GetUserProfileAsync();
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
            return View(new User());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                _logger.LogInformation("Customer/RegistrationController/Create");
                BindDropdowns();
                if (ModelState.IsValid)
                {
                    if (user.ImagePhoto != null)
                    {
                        string folder = "assets/images/users/";
                        user.ImageName = await UploadImage(folder, user.ImagePhoto);
                    }
                    else
                        user.ImageName = "sample-profile.png";

                    user.CreatedBy = userDetail.CreatedBy;
                    user.UserTypeID = (int)Common.UserType.External;
                    user.IsActive = true;
                    var result = _accountService.CreateUser(user);
                    if (user.RecordExist)
                    {
                        ModelState.AddModelError("", "User already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "User Created Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(user);
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

           var fileName = Guid.NewGuid().ToString().Substring(0,8) + "_" + file.FileName;
            folderPath += fileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return fileName;
        }

        [NonAction]
        void BindDropdowns()
        {
            var stateList = ddlService.GetRole(null);
            ViewBag.ddlRole = new SelectList(stateList, "id", "value");
        }
        public async Task<IActionResult> Edit(int uid)
        {
            try
            {
                var uDetail = await _accountService.GetUserByIdAsync(uid);
                BindDropdowns();
                return View(uDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            try
            {
                _logger.LogInformation("Customer/RegistrationController/Edit");
                BindDropdowns();
                if (ModelState.IsValid)
                {
                    if (user.ImagePhoto != null)
                    {
                        string folder = "assets/images/users/";
                        user.ImageName = await UploadImage(folder, user.ImagePhoto);
                    }
                    else
                        user.ImageName = "sample-profile.png";

                    user.CreatedBy = userDetail.CreatedBy;                
                    var result = _accountService.UpdateUser(user);
                    if (user.RecordExist)
                    {
                        ModelState.AddModelError("", "User already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "User Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
    }
}
