﻿using CSRPulse.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class ProfileController : BaseController<ProfileController>
    {
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfileController(IAccountService accountService, IWebHostEnvironment webHostEnvironment)
        {
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(int? uid = null)
        {
            _logger.LogInformation("ProfileController/Index");
            try
            {
                if (uid.HasValue)
                {
                    var uDetail = _accountService.GetUserProfileByIdAsync(uid.Value);
                    return View(uDetail);
                }
                else
                    return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpGet]
        public async Task<PartialViewResult> Edit(int id)
        {
            var uDetail = await _accountService.GetUserByIdAsync(id);
            Model.SignUp signUp = new Model.SignUp
            {
                FullName = uDetail.FullName,
                MobileNo = uDetail.MobileNo,
                EmailId = uDetail.EmailID,
                Password = uDetail.Password,
                ConfirmPassword = uDetail.Password,
                ImageName = uDetail.ImageName,
                UserID=id
            };

            return PartialView("_EditProfile", signUp);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Model.SignUp userDetail)
        {
            try
            {
               
                ModelState.Remove("UserID");
                ModelState.Remove("UserName");
                if (ModelState.IsValid)
                {
                    if (userDetail.ImagePhoto != null)
                    {
                        string folder = "assets/images/users/";
                        userDetail.ImageName = await UploadImage(folder, userDetail.ImagePhoto);
                    }               

                    var res = _accountService.UpdateUser(userDetail);
                    var msg = res == true ? "Profile updated successfully." : "Unable to update profile.";
                    return Json(new { status=res, msg= msg });
                }
                else
                    return Json(new { htmlData = ConvertViewToString("_EditProfile", userDetail, true) });             
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
