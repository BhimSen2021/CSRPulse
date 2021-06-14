using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CSRPulse.Controllers
{
    [Route("[Controller]/[action]")]
    public class VillageController : BaseController<VillageController>
    {
        private readonly IVillageServices _villageServices;
        private readonly IDropdownBindService _ddlService;
        public VillageController(IVillageServices villageServices, IDropdownBindService ddlService)
        {
            _villageServices = villageServices;
            _ddlService = ddlService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("VillageController/Index");
            try
            {
                var result = await _villageServices.GetVillageList();
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
            return View(new Village());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Village village)
        {
            try
            {
                _logger.LogInformation("VillageController/Create");
                ModelState.Remove("IsActive");
                BindDropdowns();
                if (ModelState.IsValid)
                {

                    village.CreatedBy = userDetail == null ? 1 : userDetail.UserID;
                    village.IsActive = true;
                    if (await _villageServices.RecordExist(village))
                    {
                        ModelState.AddModelError("", "Village or Village Code already exists");
                    }
                    else
                    {
                        var result = await _villageServices.CreateVillage(village);
                        if (village.RecordExist)
                        {
                            ModelState.AddModelError("", "Village already exists");
                        }
                        if (result)
                        {
                            TempData["Message"] = "Village Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(village);
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
        //    _logger.LogInformation("VillageController/UserActiveDeActive");
        //    var result = _villageServices.UserActiveDeActive(userId, IsActive);
        //    return Json(result);
        //}
        public async Task<IActionResult> Edit(int rid)
        {
            _logger.LogInformation("VillageController/Edit");
            try
            {
                BindDropdowns();
                var uDetail = await _villageServices.GetVillageById(rid);
                ViewBag.ddlDistrict = GetDistrict(uDetail.StateId, null);
                ViewBag.ddlBlock = GetBlock(uDetail.StateId, uDetail.BlockId, null);
                return View(uDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Village village)
        {
            try
            {
                _logger.LogInformation("VillageController/Edit");
                ModelState.Remove("IsActive");
                BindDropdowns();
                ViewBag.ddlDistrict = GetDistrict(village.StateId, null);
                ViewBag.ddlBlock = GetBlock(village.StateId, village.BlockId,null);
                if (ModelState.IsValid)
                {
                    village.UpdatedBy = userDetail == null ? 1 : userDetail.UserID;
                    village.UpdatedOn = DateTime.Now;
                    var result = await _villageServices.UpdateVillage(village);
                    if (village.RecordExist)
                    {
                        ModelState.AddModelError("", "Village already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "Village Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(village);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }


        public JsonResult BindDistrict(int stateId)
        {
            _logger.LogInformation($"VillageController/BindDistrict/stateId={stateId}");
            try
            {
                return Json(GetDistrict(stateId, null));
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        public JsonResult BindBlock(int stateId, int districtId)
        {
            _logger.LogInformation($"VillageController/BindBlock/stateId={stateId}/districtId={districtId}");
            try
            {
                return Json(GetBlock(stateId, districtId, null));
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        [NonAction]
        public SelectList GetDistrict(int? stateId, int? districtId)
        {
            _logger.LogInformation($"VillageController/GetDistrict/stateId={stateId}");
            try
            {
                var districtList = _ddlService.GetDistrict(stateId, null);
                var ddlDistrict = new SelectList(districtList, "id", "value");
                return ddlDistrict;
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public SelectList GetBlock(int? stateId, int? districtId, int? blockId)
        {
            _logger.LogInformation($"VillageController/GetBlock/stateId={stateId}/districtId={districtId}");
            try
            {
                var blockList = _ddlService.GetBlock(stateId, districtId, null);
                var ddlBlock = new SelectList(blockList, "id", "value");
                return ddlBlock;
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [NonAction]
        void BindDropdowns()
        {
            var stateList = _ddlService.GetStateAsync(null, null);
            ViewBag.ddlState = new SelectList(stateList, "id", "value");
        }
    }
}
