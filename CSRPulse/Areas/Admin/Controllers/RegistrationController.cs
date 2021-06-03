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

namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class RegistrationController : CSRPulse.Controllers.BaseController<RegistrationController>
    {
        private readonly IRegistrationService _registrationService;
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RegistrationController(IRegistrationService registrationService, IAccountService accountService, IWebHostEnvironment webHostEnvironment) : base()
        {
            _registrationService = registrationService;
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Admin/RegistrationController/Index");
            try
            {
                var result = await _accountService.GetUserAsync();
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
            return View(new SignUp());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SignUp singUp)
        {
            try
            {
                _logger.LogInformation("Admin/RegistrationController/Create");

                if (ModelState.IsValid)
                {
                    if (singUp.ImagePhoto != null)
                    {
                        string folder = "assets/images/users/";
                        singUp.ImageName = await UploadImage(folder, singUp.ImagePhoto);
                    }
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

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, folderPath)))
                Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, folderPath));

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return folderPath;
        }

    }
}
