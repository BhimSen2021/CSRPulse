using CSRPulse.Common;
using CSRPulse.Controllers;
using CSRPulse.Model;
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

        public async Task<PartialViewResult> GetProcessWorkFlow(int processId)
        {
            _logger.LogInformation("ProcessWorkFlow/GetProcessWorkFlow");

            ViewBag.ddlRole = new SelectList(_dropdownBindService.GetRole(null), "id", "value");

            var processes = await _processSetupServices.GetProcessSetupById(processId);
            ProcessSetupModel setupModel = new ProcessSetupModel();
            setupModel.ProcessId = processId;
            setupModel.RevisionNo = 1;
            setupModel.processSetupList = MakeProcesRecords(processes, processId);
            return PartialView("_WorkFlowList", setupModel);
        }

        public async Task<IActionResult> UpdateSkillValue(IEnumerable<ProcessSetupModel> processSetups)
        {
            _logger.LogInformation("ProcessWorkFlow/UpdateSkillValue");
            try
            {
                var getProcess = await _processSetupServices.GetProcessSetupById(1);
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);

                throw;
            }
        }
        public async Task<IActionResult> UpdateProcessWorkflow(ProcessSetupModel processes)
        {
            _logger.LogInformation("ProcessWorkFlow/UpdateSkillValue");
            try
            {
                var listProces = processes.processSetupList.Where(s => s.PrimaryRoleId > 0).ToList();
                var getProcess = await _processSetupServices.UpdateProcessSetup(listProces);
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);

                throw;
            }
        }

        private List<ProcessSetup> MakeProcesRecords(List<ProcessSetup> processSetups, int processId)
        {
            for (int i = processSetups.Count; i < 6; i++)
            {
                processSetups.Add(new ProcessSetup
                {
                    ProcessId = processId,
                    Sequence = (i + 1)

                });
            }
            return processSetups;
        }
    }
}
