using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    [Route("[Controller]/[action]")]
    public class StateController : CSRPulse.Controllers.BaseController<StateController>
    {
        private readonly IStateServices _stateServices;
        public StateController(IStateServices stateServices)
        {
            _stateServices = stateServices;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("StateController/Index");
            try
            {
                var result = await _stateServices.GetStateList();
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
            return View(new State());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(State state)
        {
            try
            {
                _logger.LogInformation("StateController/Create");
                ModelState.Remove("IsActive");
                if (ModelState.IsValid)
                {

                    state.CreatedBy = userDetail == null ? 1 : userDetail.UserID;
                    state.IsActive = true;
                    if (await _stateServices.RecordExist(state))
                    {
                        ModelState.AddModelError("", "State or State Code already exists");
                    }
                    else
                    {
                        var result = await _stateServices.CreateState(state);
                        if (state.RecordExist)
                        {
                            ModelState.AddModelError("", "State already exists");
                        }
                        if (result)
                        {
                            TempData["Message"] = "State Created Successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(state);
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
        //    _logger.LogInformation("StateController/UserActiveDeActive");
        //    var result = _stateServices.UserActiveDeActive(userId, IsActive);
        //    return Json(result);
        //}
        public async Task<IActionResult> Edit(int rid)
        {
            try
            {
                var uDetail = await _stateServices.GetStateById(rid);
                return View(uDetail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(State state)
        {
            try
            {
                _logger.LogInformation("StateController/Edit");
                ModelState.Remove("IsActive");
                if (ModelState.IsValid)
                {
                    state.UpdatedBy = userDetail == null ? 1 : userDetail.UserID;
                    state.UpdatedOn = DateTime.Now;
                    var result = await _stateServices.UpdateState(state);
                    if (state.RecordExist)
                    {
                        ModelState.AddModelError("", "State already exists");
                    }
                    if (result)
                    {
                        TempData["Message"] = "State Updated Successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(state);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
    }
}
