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
            var setupModel = new ProcessSetupModel();

            var processes = await _processSetupServices.GetProcessSetupById(processId);

            if (processes != null && processes.Count > 0)
                setupModel.RevisionNo = GetRevisionNo(processes, processId);
            else
                setupModel.RevisionNo = 1;
            setupModel.ProcessId = processId;

            setupModel.processSetupList = MakeProcesRecords(processes, processId, setupModel.RevisionNo);
            return PartialView("_WorkFlowList", setupModel);
        }

        public async Task<IActionResult> UpdateSkipValue(ProcessSetupModel processSetup)
        {
            _logger.LogInformation("ProcessWorkFlow/UpdateSkipValue");
            try
            {
                bool result = false;
                if (processSetup.processSetupList != null)
                {
                    foreach (var process in processSetup.processSetupList)
                    {
                        process.UpdatedBy = userDetail.UserID;
                        process.UpdatedOn = DateTime.Now;
                        result = await _processSetupServices.UpdateProcessSetup(process);
                    }
                }

                return Json(new { success = result, htmlData = ConvertViewToString("_WorkFlowList", processSetup, true) });
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
                var listProces = processes.processSetupList.Where(s => s.PrimaryRoleId > 0 && s.LevelId > 0).ToList();
                if (listProces.Count > 0)
                {
                    listProces.ToList().ForEach(h =>
                    {
                        h.CreatedOn = DateTime.Now;
                        h.CreatedBy = userDetail.UserID;
                    });

                    var result = await _processSetupServices.CreateProcessSetup(listProces);

                    return Json(new { success = result, msg = 1, htmlData = ConvertViewToString("_WorkFlowList", processes, true) });
                }
                else
                {
                    return Json(new { success = false, msg = 0, htmlData = ConvertViewToString("_WorkFlowList", processes, true) });
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);

                throw;
            }
        }

        private List<ProcessSetup> MakeProcesRecords(List<ProcessSetup> processSetups, int processId, int revisionNo)
        {
            for (int i = processSetups.Count; i < 6; i++)
            {
                processSetups.Add(new ProcessSetup
                {
                    ProcessId = processId,
                    RevisionNo = revisionNo,
                    Sequence = (i + 1)

                });
            }
            return processSetups.OrderBy(s => s.Sequence).ToList();
        }


        private int GetRevisionNo(List<ProcessSetup> processSetups, int processId)
        {
            return processSetups.Where(w => w.ProcessId == processId).Select(r => r.RevisionNo).FirstOrDefault();
        }
    }
}
