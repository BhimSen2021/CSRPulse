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
            bool IsMaintenance = false;
            _logger.LogInformation("MaintenanceController/MaintenanceAsync");
            try
            {
                if (maintenance.EndDateTime != null)
                {
                    if (maintenance.StartDateTime > maintenance.EndDateTime)
                    {
                        ModelState.AddModelError("EndDateTime", "End date time should be greater then start date time");
                    }
                }

                if (ModelState.IsValid)
                {
                    IsMaintenance = await _maintenanceService.GoUnderMaintenanceAsync(maintenance);
                    var sendEmail = await _maintenanceService.SendEmailAsync(maintenance);

                }
                else
                {
                    return Json(new { htmlData = ConvertViewToString("_Maintenance", maintenance, true) });
                }
            }
            catch (Exception ex)
            {
                IsMaintenance = false;
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
            return Json(new { isMaintenance = IsMaintenance, isDown = maintenance.IsMaintenance, htmlData = ConvertViewToString("_Maintenance", maintenance, true) });
        }
    }
}
