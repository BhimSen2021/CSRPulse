using CSRPulse.Controllers;
using CSRPulse.Models;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    [Route("[Controller]/[action]")]
    public class ProcessWorkFlowController : BaseController<ProcessWorkFlowController>
    {
        private readonly IProcessSetupServices _processSetupServices;
        private readonly IDropdownBindService _dropdownBindService;

        public ProcessWorkFlowController(IProcessSetupServices processSetupServices, IDropdownBindService dropdownBindService)
        {
            _processSetupServices = processSetupServices;
            _dropdownBindService = dropdownBindService;
        }
        public IActionResult Index()
        {
            ProcessSetupModel processSetup = new ProcessSetupModel();
            var process = _dropdownBindService.GetProcess(null);
            ViewBag.ddlProcess = new SelectList(process, "id", "value");
            return View();
        }

        public PartialViewResult GetProcessWorkFlow(int processId)
        {
            _logger.LogInformation("ProcessWorkFlow/GetProcessWorkFlow");
            List <ProcessSetupModel> setupModels = new List<ProcessSetupModel>
            {

                    new ProcessSetupModel
                    {
                        ProcessId=1,LevelName="Initiator",PrimaryRole="Manager",SecondaryRole="Asst. Manager",Sequence=1,Skip=false
                    },
                    new ProcessSetupModel
                    {
                        ProcessId=1,LevelName="Forwarder",PrimaryRole="Jr. Manager",SecondaryRole="Jr. Asst. Manager",Sequence=2,Skip=false
                    }
            };

            //    var getProcess =await _processSetupServices.GetProcessSetupById(processId);
            return PartialView("_WorkFlowList", setupModels);
        }

       public async  Task<IActionResult> UpdateSkillValue(IEnumerable<ProcessSetupModel> processSetups) {
            _logger.LogInformation("ProcessWorkFlow/UpdateSkillValue");
            try
            {
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);

                throw;
            }
        }
    }
}
