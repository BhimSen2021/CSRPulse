using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class DepartmentController : BaseController<DepartmentController>
    {
        private readonly IDepartmentServices _departmentServices;

        public DepartmentController(IDepartmentServices departmentServices)
        {
            _departmentServices = departmentServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("DepartmentController/Index");
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        [HttpGet]
        public async Task<PartialViewResult> GetDepartmentList(Department department)
        {
            _logger.LogInformation("DepartmentController/GetDepartmentList");
            try
            {
                var result = await _departmentServices.GetDepartmentsAsync(department);
                return PartialView("_DepartmentList", result);
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
            return View(new Department());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            try
            {
                _logger.LogInformation("DepartmentController/Create");               
                if (ModelState.IsValid)
                {
                    department.CreatedBy = userDetail.UserID;
                    department.CreatedRid = userDetail.RoleId;
                    department.CreatedRname = userDetail.RoleName;
                    department.IsActive = true;
                    if (await _departmentServices.RecordExist(department))
                    {
                        ModelState.AddModelError("", "Department already exists");
                    }
                    else
                    {
                        var result = await _departmentServices.CreateDepartment(department);
                        if (department.RecordExist)
                        {
                            ModelState.AddModelError("", "Department already exists");
                        }
                        if (result)
                        {
                            TempData["Message"] = "Department Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }


        public async Task<IActionResult> Edit(int departmentId)
        {
            try
            {
                var dDetail = await _departmentServices.GetDepartmentById(departmentId);
                return View(dDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Department department)
        {
            try
            {
                _logger.LogInformation("DepartmentController/Edit");
                ModelState.Remove("IsActive");
                if (ModelState.IsValid)
                {
                    department.UpdatedBy = userDetail.UserID;
                    department.UpdatedRid = userDetail.RoleId;
                    department.UpdatedRname = userDetail.RoleName;
                    department.UpdatedOn = DateTime.Now;
                    var result = await _departmentServices.UpdateDepartment(department);
                    if (department.RecordExist)
                    {
                        ModelState.AddModelError("", "Department already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "Department Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        public JsonResult ActiveDeActive(int id, bool isChecked)
        {
            _logger.LogInformation("DepartmentController/ActiveDeActive");
            var result = _departmentServices.ActiveDeActive(id, isChecked);
            return Json(result);

        }

    }
}
