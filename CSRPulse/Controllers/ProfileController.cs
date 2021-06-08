using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(int? uid=null)
        {
            if (uid.HasValue)
            {
                var uDetail = _accountService.GetUserByIdAsync(uid.Value);
                Model.UserDetail userDetail = new Model.UserDetail
                {
                     UserID=uid.Value
                };
                return View(userDetail);
            }
            else
            return View();
        }
    }
}
