using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Areas.Admin.Controllers
{
    [Route("[Controller]/[action]")]
    public class RoleController : CSRPulse.Controllers.BaseController<UserController>
    {
        private readonly IRoleServices _roleServices;
        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("RoleController/Index");
            try
            {
                var result =await _roleServices.GetRoles();
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
            return View(new Role());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            try
            {
                _logger.LogInformation("RoleController/Create");
                ModelState.Remove("IsActive");
                if (ModelState.IsValid)
                {

                    role.CreatedBy = userDetail.CreatedBy;
                    role.IsActive = true;
                    var result = await _roleServices.CreateRole(role);
                    if (role.RecordExist)
                    {
                        ModelState.AddModelError("", "Role already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "Role Created Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(role);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        //[HttpPost]
        //public JsonResult UserActiveDeActive(int userId, bool IsActive)
        //{
        //    _logger.LogInformation("RoleController/UserActiveDeActive");
        //    var result = _roleServices.UserActiveDeActive(userId, IsActive);
        //    return Json(result);
        //}
        public async Task<IActionResult> Edit(int rid)
        {
            try
            {
                var uDetail = await _roleServices.GetRolesById(rid);
                return View(uDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Role role)
        {
            try
            {
                _logger.LogInformation("RoleController/Edit");
                ModelState.Remove("IsActive");
                if (ModelState.IsValid)
                {
                    role.CreatedBy = userDetail.CreatedBy;
                    var result =await _roleServices.UpdateRole(role);
                    if (role.RecordExist)
                    {
                        ModelState.AddModelError("", "Role already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "Role Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(role);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
    }
}
