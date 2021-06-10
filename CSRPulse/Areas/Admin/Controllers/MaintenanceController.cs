using CSRPulse.Controllers;
using CSRPulse.Model;
using CSRPulse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[Controller]/[action]")]
    public class MaintenanceController : BaseController<MaintenanceController>
    {
        private readonly IMaintenanceService _maintenanceService;
        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Maintenance()
        {
            return await Task.FromResult((IActionResult)PartialView("_Maintenance", new Maintenance()));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> MaintenanceAsync(Maintenance maintenance)
        {
            bool Issuccess = false;
            _logger.LogInformation("MaintenanceController/MaintenanceAsync");
            try
            {
                if (maintenance.StartDateTime > maintenance.EndDateTime)
                {
                    ModelState.AddModelError("EndDateTime", "End date time should be greater then start date time");                  
                }

                if (ModelState.IsValid)
                {                    
                    Issuccess = await _maintenanceService.GoUnderMaintenance(maintenance);
                }
                else
                {
                    return Json(new { htmlData = ConvertViewToString("_Maintenance", maintenance, true) });
                }
            }
            catch (Exception ex)
            {
                Issuccess = false;
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
            return Json(new { success = Issuccess, htmlData = ConvertViewToString("_Maintenance", maintenance, true) });
        }
    }
}
