using CSRPulse.Model;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Controllers
{
    public class ProjectController : BaseController<ProjectController>
    {
        private readonly IProjectService _projectService;


        private readonly IDropdownBindService _ddlService;
        public ProjectController(IProjectService projectService, IDropdownBindService dropdownBindService)
        {
            _projectService = projectService;
            _ddlService = dropdownBindService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("ProjectController/Index");
            try
            {
                var result = await _projectService.GetProjectAsync();
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await BindLocationAsync();
            BindDropdowns();

            var project = new Project();
            project.ProjectOtherSource = new List<ProjectOtherSource>();
            project.ProjectOtherSource.Add(new ProjectOtherSource { ProjectOtherSourceId = 0, ProjectId = 0, SourceId = 0, Amount = null, RevisionNo = 1 });

            project.ProjectInternalSource = new List<ProjectInternalSource>();
            project.ProjectInternalSource.Add(new ProjectInternalSource { ProjectInternalSourceId = 0, ProjectId = 0, SourceId = 0, Amount = null, RevisionNo = 1 });

            HttpContext.Session.SetComplexData("project", project);
            return View(project);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Project project, string ButtonType)
        {
            try
            {
                _logger.LogInformation("ProjectController/Create");

                if (ButtonType == "AddOS")
                {
                    if (project.ProjectOtherSource == null)
                        project.ProjectOtherSource = new List<ProjectOtherSource>();

                    var totalAmount = project.ProjectOtherSource.Sum(x => x.Amount);

                    project.ProjectOtherSource.ForEach(o =>
                    {
                        o.Per = Convert.ToString(Math.Round((Convert.ToDecimal
                            (o.Amount * 100 / project.TotalBudget)), 2)) + "%";
                    });
                    if (project.OtherSource > totalAmount)
                    {
                        project.ProjectOtherSource.Add(new ProjectOtherSource { ProjectOtherSourceId = 0, ProjectId = project.ProjectId, SourceId = 0, Amount = null, RevisionNo = 1, Per = "0%" });
                    }

                    HttpContext.Session.SetComplexData("project", project);
                    return Json(new { msg = "addOS", htmlData = ConvertViewToString("_OtherSourcesContribution", project, true) });
                }
                else if (ButtonType == "AddIS")
                {
                    if (project.ProjectInternalSource == null)
                        project.ProjectInternalSource = new List<ProjectInternalSource>();

                    var totalAmount = project.ProjectInternalSource.Sum(x => x.Amount);

                    project.ProjectInternalSource.ForEach(o =>
                    {
                        o.Per = Convert.ToString(Math.Round((Convert.ToDecimal
                            (o.Amount * 100 / project.TotalBudget)), 2)) + "%";
                    });
                    if (project.TrustContribution > totalAmount)
                    {
                        project.ProjectInternalSource.Add(new ProjectInternalSource { ProjectInternalSourceId = 0, ProjectId = project.ProjectId, SourceId = 0, Amount = null, RevisionNo = 1, Per = "0%" });
                    }

                    HttpContext.Session.SetComplexData("project", project);
                    return Json(new { msg = "addIS", htmlData = ConvertViewToString("_TrustContribution", project, true) });
                }

                ModelState.Remove("IsActive");
                if (ModelState.IsValid)
                {
                    project.CreatedBy = userDetail == null ? 1 : userDetail.UserID;
                    project.IsActive = true;

                    project.ProjectOtherSource = project.ProjectOtherSource.Where(x => x.Amount > 0).ToList();
                    project.ProjectInternalSource = project.ProjectInternalSource.Where(x => x.Amount > 0).ToList();

                    // Make project Intervention Report
                    DataTable projectIReportDt = Common.ProjectReport.MakeProjectReport(project.ProjectId, project.StartDate ?? DateTime.UtcNow, project.EndDate ?? DateTime.UtcNow, project.ReportType);
                    var ProjectIReportList = ConvertProjectIReportList(projectIReportDt);
                    if (project.ProjectInterventionReport == null)
                        project.ProjectInterventionReport = new List<ProjectInterventionReport>();
                    project.ProjectInterventionReport = ProjectIReportList;

                    // Make project ProjectReport
                    if (project.ProjectReport == null)
                        project.ProjectReport = new List<ProjectReport>();
                    project.ProjectReport = MakeProjectReport(ProjectIReportList);

                    // Make project Financial report
                    if (project.ProjectFinancialReport == null)
                        project.ProjectFinancialReport = new List<ProjectFinancialReport>();
                    project.ProjectFinancialReport = MakeProjectFReport(ProjectIReportList);

                    // Make Project Location
                    if (project.hdnLocationIds.Length > 0)
                    {
                        if (project.ProjectLocation == null)
                            project.ProjectLocation = new List<ProjectLocation>();
                        project.ProjectLocation = MakeProjectLocation(project.hdnLocationIds);

                    }
                    var result = await _projectService.CreateStateAsync(project);
                    if (project.IsExist)
                    {
                        ModelState.AddModelError("ProjectName", "Project Name already exists");
                    }

                    if (result.ProjectId > 0)
                    {
                        HttpContext.Session.Remove("project");
                        TempData["Message"] = "Project Created Successfully.";
                        return Json(new { msg = "save", htmlData = ConvertViewToString("_OtherSourcesContribution", project, true) });
                    }
                }
                await BindLocationAsync();
                BindDropdowns();
                return Json(new { msg = "", htmlData = ConvertViewToString("_Create", project, true) });
            }
            catch (Exception ex)
            {
                HttpContext.Session.Remove("project");
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }
        public async Task<IActionResult> Edit(int pid)
        {
            try
            {
                await BindLocationAsync();
                BindDropdowns();
                var project = _projectService.GetProjectById(pid);
                ViewBag.tvLDetails = await _projectService.GetTvLocationDetails(project.ProjectId, project.LocationLavel);

                BindNestedDropdown(project.ThemeId);


                HttpContext.Session.SetComplexData("project", project);
                // Make Percentage for project other source breakup
                project.ProjectOtherSource.ForEach(o =>
                {
                    o.Per = Convert.ToString(Math.Round((Convert.ToDecimal
                    (o.Amount * 100 / project.TotalBudget)), 2)) + "%";
                });

                // Make Percentage for project other source breakup
                project.ProjectInternalSource.ForEach(i =>
                {
                    i.Per = Convert.ToString(Math.Round((Convert.ToDecimal
                    (i.Amount * 100 / project.TotalBudget)), 2)) + "%";
                });
                project.hdnLocationIds = SetProjectLocation(project.ProjectLocation);
                return View(project);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Project project, string ButtonType)
        {
            try
            {
                _logger.LogInformation("ProjectController/Edit");
                if (ButtonType == "AddOS")
                {
                    if (project.ProjectOtherSource == null)
                        project.ProjectOtherSource = new List<ProjectOtherSource>();

                    var totalAmount = project.ProjectOtherSource.Sum(x => x.Amount);

                    project.ProjectOtherSource.ForEach(o =>
                    {
                        o.Per = Convert.ToString(Math.Round((Convert.ToDecimal
                            (o.Amount * 100 / project.TotalBudget)), 2)) + "%";
                    });
                    if (project.OtherSource > totalAmount)
                    {
                        project.ProjectOtherSource.Add(new ProjectOtherSource { ProjectOtherSourceId = 0, ProjectId = project.ProjectId, SourceId = 0, Amount = null, RevisionNo = 1, Per = "0%" });
                    }

                    HttpContext.Session.SetComplexData("project", project);
                    return Json(new { msg = "addOS", htmlData = ConvertViewToString("_OtherSourcesContribution", project, true) });
                }
                else if (ButtonType == "AddIS")
                {
                    if (project.ProjectInternalSource == null)
                        project.ProjectInternalSource = new List<ProjectInternalSource>();

                    var totalAmount = project.ProjectInternalSource.Sum(x => x.Amount);

                    project.ProjectInternalSource.ForEach(o =>
                    {
                        o.Per = Convert.ToString(Math.Round((Convert.ToDecimal
                            (o.Amount * 100 / project.TotalBudget)), 2)) + "%";
                    });
                    if (project.TrustContribution > totalAmount)
                    {
                        project.ProjectInternalSource.Add(new ProjectInternalSource { ProjectInternalSourceId = 0, ProjectId = project.ProjectId, SourceId = 0, Amount = null, RevisionNo = 1, Per = "0%" });
                    }

                    HttpContext.Session.SetComplexData("project", project);
                    return Json(new { msg = "addIS", htmlData = ConvertViewToString("_TrustContribution", project, true) });
                }

                ModelState.Remove("IsActive");
                if (ModelState.IsValid)
                {
                    project.UpdatedBy = userDetail.UserID;
                    project.UpdatedOn = DateTime.UtcNow;

                    project.IsActive = true;

                    if (project.ProjectOtherSource != null)
                        project.ProjectOtherSource = project.ProjectOtherSource.Where(x => x.Amount > 0).ToList();
                    else
                        project.ProjectOtherSource = new List<ProjectOtherSource>();

                    if (project.ProjectInternalSource != null)
                        project.ProjectInternalSource = project.ProjectInternalSource.Where(x => x.Amount > 0).ToList();
                    else
                        project.ProjectInternalSource = new List<ProjectInternalSource>();

                    // Make Project Location
                    if (project.hdnLocationIds.Length > 0)
                    {
                        if (project.ProjectLocation == null)
                            project.ProjectLocation = new List<ProjectLocation>();
                        project.ProjectLocation = MakeProjectLocation(project.hdnLocationIds);

                    }
                    else
                        project.ProjectLocation = new List<ProjectLocation>();

                    var result = await _projectService.UpdateProjectAsync(project);
                    if (result)
                        TempData["Message"] = "Project Updated Successfully.";

                    else
                        TempData["Error"] = "Project Updation Failed.";

                    HttpContext.Session.Remove("project");
                    return Json(new { msg = "save", htmlData = ConvertViewToString("_Edit", project, true) });
                }

                await BindLocationAsync();
                ViewBag.tvLDetails = await _projectService.GetTvLocationDetails(project.ProjectId, project.LocationLavel);
                BindDropdowns();
                BindNestedDropdown(project.ThemeId);

                return Json(new { msg = "", htmlData = ConvertViewToString("_Edit", project, true) });

            }
            catch (Exception ex)
            {
                HttpContext.Session.Remove("project");
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public async Task<IActionResult> SaveLocationDetail(int projectId, int lLevel, string locationIds)
        {
            List<ProjectLocationDetail> locationDetails = new List<ProjectLocationDetail>();
            locationDetails = MakeProjectLocationDetail(projectId, locationIds, lLevel);
            var flag = await _projectService.AddLocationDetails(locationDetails, projectId);
            var result = _projectService.GetLocationDetails(projectId, lLevel);
          
            return PartialView("_LocationDetail", result);
           
        }
        public PartialViewResult RemoveOS(int srNo)
        {
            var project1 = HttpContext.Session.GetComplexData<Project>("project");

            project1.ProjectOtherSource.RemoveAt(srNo);
            HttpContext.Session.SetComplexData("project", project1);
            return PartialView("_OtherSourcesContribution", project1);

        }
        public PartialViewResult RemoveIS(int srNo)
        {
            var project1 = HttpContext.Session.GetComplexData<Project>("project");

            project1.ProjectInternalSource.RemoveAt(srNo);
            HttpContext.Session.SetComplexData("project", project1);
            return PartialView("_TrustContribution", project1);
        }

        [HttpPost]
        public JsonResult ActiveDeActive(int id, bool isChecked)
        {
            _logger.LogInformation("ProjectController/ActiveDeActive");
            var result = _projectService.ActiveDeActive(id, isChecked);
            return Json(result);

        }

        [NonAction]
        void BindDropdowns()
        {
            var themeList = _ddlService.GetTheme(null);
            ViewBag.ddlTheme = new SelectList(themeList, "id", "value");
            var partnerList = _ddlService.GetPartners(null);
            ViewBag.ddlPartner = new SelectList(partnerList, "id", "value");

            var sourceList = _ddlService.GetSource(null);
            ViewBag.ddlOtherSource = new SelectList(sourceList, "id", "value");

            var PMList = _ddlService.GetUsers((int)Common.UserRole.ProgramManager);
            ViewBag.ddlPM = new SelectList(PMList, "id", "value");

        }

        [NonAction]
        async Task BindLocationAsync()
        {
            ViewBag.tvState = await _ddlService.GetStateLocationAsync(null);
            ViewBag.tvDistrict = await _ddlService.GetDistrictLocationAsync(null, null);

        }
        [NonAction]
        void BindNestedDropdown(int themeId)
        {
            var subThemeList = _ddlService.GetSubTheme(themeId, null);
            ViewBag.ddlSubTheme = new SelectList(subThemeList, "id", "value");
        }

        private List<ProjectInterventionReport> ConvertProjectIReportList(DataTable ProjectIReportdt)
        {
            List<ProjectInterventionReport> qtrList = new List<ProjectInterventionReport>();
            for (int i = 0; i < ProjectIReportdt.Rows.Count; i++)
            {
                qtrList.Add(new ProjectInterventionReport
                {
                    ProjectId = Convert.ToInt32(ProjectIReportdt.Rows[i]["ProjectId"]),
                    ReportNo = Convert.ToInt32(ProjectIReportdt.Rows[i]["ReportNo"]),
                    ReportName = Convert.ToString(ProjectIReportdt.Rows[i]["ReportName"]),
                    ProjectYear = Convert.ToString(ProjectIReportdt.Rows[i]["ProjectYear"]),
                    StartDate = Convert.ToDateTime(ProjectIReportdt.Rows[i]["StartDate"]),
                    EndDate = Convert.ToDateTime(ProjectIReportdt.Rows[i]["EndDate"]),
                    DueDate = Convert.ToDateTime(ProjectIReportdt.Rows[i]["DueDate"]),
                    Status = Convert.ToInt32(ProjectIReportdt.Rows[i]["Status"]),
                });
            }
            return qtrList;
        }

        private List<ProjectReport> MakeProjectReport(List<ProjectInterventionReport> reports)
        {
            var reportList = new List<ProjectReport>();
            foreach (var report in reports)
            {
                reportList.Add(new ProjectReport
                {
                    ProjectId = report.ProjectId,
                    ReportNo = report.ReportNo,
                    YearName = report.ProjectYear,
                    ReportName = report.ReportName,
                    YearNo = Convert.ToInt32(report.ProjectYear.Replace("Year", "", StringComparison.InvariantCultureIgnoreCase).Trim())
                });
            }
            return reportList;
        }

        private List<ProjectFinancialReport> MakeProjectFReport(List<ProjectInterventionReport> reports)
        {
            var financialReports = new List<ProjectFinancialReport>();
            foreach (var report in reports)
            {
                financialReports.Add(new ProjectFinancialReport
                {
                    ProjectId = report.ProjectId,
                    ReportNo = report.ReportNo,
                    DueDate = report.DueDate,
                    ReportName = report.ReportName,
                    ProjectYear = report.ProjectYear,
                    StartDate = report.StartDate,
                    EndDate = report.EndDate,
                    Status = report.Status,
                });
            }
            return financialReports;
        }

        private List<ProjectLocation> MakeProjectLocation(string locationIds)
        {
            List<ProjectLocation> projectLocations = new List<ProjectLocation>();
            string[] arr = locationIds.Split(',');
            string[] val = { };
            for (int i = 0; i < arr.Length; i++)
            {
                val = arr[i].Split(':');
                projectLocations.Add(new ProjectLocation
                {
                    StateId = val[0] == "" ? 0 : Convert.ToInt32(val[0]),
                    DistrictId = val[1] == "" ? 0 : Convert.ToInt32(val[1]),
                });
            }

            return projectLocations;
        }

        private List<ProjectLocationDetail> MakeProjectLocationDetail(int projectId, string locationIds, int lLevel)
        {
            List<ProjectLocationDetail> projectLocations = new List<ProjectLocationDetail>();
            string[] arr = locationIds.Split(',');
            string[] val = { };
            for (int i = 0; i < arr.Length; i++)
            {
                val = arr[i].Split(':');
                if (lLevel == 2)
                {
                    projectLocations.Add(new ProjectLocationDetail
                    {
                        ProjectId = projectId,
                        StateId = val[0] == "" ? 0 : Convert.ToInt32(val[0]),
                        DistrictId = val[1] == "" ? 0 : Convert.ToInt32(val[1]),
                        BlockId = null,
                        VillageId = null
                    });
                }
                else if (lLevel == 3)
                {
                    projectLocations.Add(new ProjectLocationDetail
                    {
                        ProjectId = projectId,
                        StateId = val[0] == "" ? 0 : Convert.ToInt32(val[0]),
                        DistrictId = val[1] == "" ? 0 : Convert.ToInt32(val[1]),
                        BlockId = val[2] == "" ? 0 : Convert.ToInt32(val[2]),
                        VillageId = null
                    });
                }
                else if (lLevel == 4)
                {
                    projectLocations.Add(new ProjectLocationDetail
                    {
                        ProjectId = projectId,
                        StateId = val[0] == "" ? 0 : Convert.ToInt32(val[0]),
                        DistrictId = val[1] == "" ? 0 : Convert.ToInt32(val[1]),
                        BlockId = val[2] == "" ? 0 : Convert.ToInt32(val[2]),
                        VillageId = val[3] == "" ? 0 : Convert.ToInt32(val[3]),
                    });
                }
                else
                {
                    projectLocations.Add(new ProjectLocationDetail
                    {
                        ProjectId = projectId,
                        StateId = 0,
                        DistrictId = 0,
                        BlockId = null,
                        VillageId = null
                    });
                }

            }

            return projectLocations;
        }
        private string SetProjectLocation(List<ProjectLocation> projectLocations)
        {
            string locationIds = string.Empty;
            foreach (var item in projectLocations)
            {
                locationIds += item.StateId + ":" + item.DistrictId + ",";
            }
            locationIds = locationIds.TrimEnd(',');
            return locationIds;
        }
        [HttpGet]
        public JsonResult BindSubThemeDropdown(int themeId)
        {
            _logger.LogInformation("ProjectController/BindActivityDropdown");
            var selectListModels = _ddlService.GetSubTheme(themeId, null).ToList();
            return Json(new SelectList(selectListModels, "id", "value"));
        }


        #region Report
        public async Task<PartialViewResult> GetReportAsync(int projectId)
        {
            var interventionReports = await _projectService.GetInterventionReportAsync(projectId);
            return PartialView("_ProjectInterventionReport", interventionReports);

        }
        #endregion
    }
}
