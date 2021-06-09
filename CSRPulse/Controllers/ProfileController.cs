using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class ProfileController : BaseController<ProfileController>
    {
        private readonly IAccountService _accountService;
        public ProfileController(IAccountService accountService)
        {
            _accountService = accountService;
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
            return PartialView("_EditProfile", uDetail);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Model.User userDetail)
        {
            try
            {
                ModelState.Remove("Password");
                ModelState.Remove("UserID");
                ModelState.Remove("UserName");
                if (ModelState.IsValid)
                {                    
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
    }
}
