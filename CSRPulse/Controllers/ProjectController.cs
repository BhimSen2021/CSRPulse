using CSRPulse.Common;
using CSRPulse.Model;
using CSRPulse.Services;
using CSRPulse.Services.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSRPulse.Controllers
{
    public class ProjectController : BaseController<ProjectController>
    {
        private readonly IProjectService _projectService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDropdownBindService _ddlService;
        public ProjectController(IProjectService projectService, IDropdownBindService dropdownBindService, IWebHostEnvironment webHostEnvironment)
        {
            _projectService = projectService;
            _ddlService = dropdownBindService;
            _webHostEnvironment = webHostEnvironment;
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
            try
            {
                _logger.LogInformation("ProjectController/Create");
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
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;

            }
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
                    DataTable projectIReportDt = ReportHelper.MakeProjectReport(project.ProjectId, project.StartDate ?? DateTime.UtcNow, project.EndDate ?? DateTime.UtcNow, project.ReportType);
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

                if (project.ProjectDocument == null)
                    project.ProjectDocument = new List<ProjectDocument>();
                project.ProjectDocument = await _projectService.GetDocumentList(project.ProjectId, (int)Common.ProcessDocument.DocumentProject);

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
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
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
                    return Json(new { msg = "save", htmlData = ConvertViewToString("_ProjectDetail", project, true) });
                }

                await BindLocationAsync();
                ViewBag.tvLDetails = await _projectService.GetTvLocationDetails(project.ProjectId, project.LocationLavel);
                BindDropdowns();
                BindNestedDropdown(project.ThemeId);

                return Json(new { msg = "", htmlData = ConvertViewToString("_ProjectDetail", project, true) });

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
            try
            {
                List<ProjectLocationDetail> locationDetails = new List<ProjectLocationDetail>();
                locationDetails = MakeProjectLocationDetail(projectId, locationIds, lLevel);
                var flag = await _projectService.AddLocationDetails(locationDetails, projectId);
                var result = _projectService.GetLocationDetails(projectId, lLevel);

                return PartialView("_LocationDetail", result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }
        public PartialViewResult RemoveOS(int srNo)
        {
            try
            {
                var project1 = HttpContext.Session.GetComplexData<Project>("project");

                project1.ProjectOtherSource.RemoveAt(srNo);
                HttpContext.Session.SetComplexData("project", project1);
                return PartialView("_OtherSourcesContribution", project1);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }
        public PartialViewResult RemoveIS(int srNo)
        {
            try
            {
                var project1 = HttpContext.Session.GetComplexData<Project>("project");

                project1.ProjectInternalSource.RemoveAt(srNo);
                HttpContext.Session.SetComplexData("project", project1);
                return PartialView("_TrustContribution", project1);
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
            arr = arr.Distinct().ToArray();
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

        #region Document
        public async Task<IActionResult> SaveDocumentCommunication(Project project, string ButtonType)
        {
            try
            {
                if (ButtonType == "document")
                {
                    if (project.ProjectDocument.Where(x => (x.ServerDocumentName == null && x.DocumentFile == null) && x.Mandatory == true).Any())
                        return Json(new { flag = 2, type = 1, msg = "select all mandatory documents", htmlData = ConvertViewToString("_DocumentList", project, true) });

                    #region Check Mime Type
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < project.ProjectDocument.Count; i++)
                    {
                        if (project.ProjectDocument[i].DocumentFile != null)
                        {
                            if (!ValidateFileMimeType(project.ProjectDocument[i].DocumentFile))
                            {
                                stringBuilder.Append($"# {i + 1} The file format of the {project.ProjectDocument[i].DocumentFile.FileName} file is incorrect");
                                stringBuilder.Append("<br>");
                            }
                        }
                    }

                    if (stringBuilder.ToString() != "")
                    {
                        return Json(new { flag = 2, type = 1, msg = stringBuilder.ToString(), htmlData = ConvertViewToString("_DocumentList", project, true) });
                    }
                    #endregion

                    for (int i = 0; i < project.ProjectDocument.Count; i++)
                    {
                        var document = new ProjectDocument();
                        document.ProjectId = project.ProjectId;
                        document.DocumentId = project.ProjectDocument[i].DocumentId;
                        document.ProjectDocumentId = project.ProjectDocument[i].ProjectDocumentId;

                        if (project.ProjectDocument[i].DocumentFile != null)
                        {                           
                            var filePath = DocumentUploadFilePath.ProjectFilePath;
                            document.DocumentName = project.ProjectDocument[i].DocumentFile.FileName;
                            document.ServerDocumentName = await UploadDocument(filePath, project.ProjectDocument[i].DocumentFile);
                        }
                        if (project.ProjectDocument[i].ProjectDocumentId == 0)
                            await _projectService.SaveDocument(document);
                        else
                        {
                            if (project.ProjectDocument[i].DocumentFile != null)
                            {
                                await _projectService.UpdateDocument(document);
                            }
                        }
                    }

                    var documents = await _projectService.GetDocumentList(project.ProjectId, (int)Common.ProcessDocument.DocumentProject);
                    project.ProjectDocument = documents;

                    return Json(new { flag = 1, type = 1, msg = "Documents uploaded successfully", htmlData = ConvertViewToString("_DocumentList", project, true) });
                }
                else
                {
                    project.Communication.ProjectId = project.ProjectId;
                    project.Communication.CreatedBy = userDetail.UserID;
                    project.Communication.CreatedOn = DateTime.UtcNow;
                    project.Communication.IsActive = true;
                    await _projectService.SaveCommunication(project.Communication);

                    project.ProjectCommunication = await _projectService.GetCommunications(project.ProjectId, true);

                    return Json(new { flag = 1, type = 2, htmlData = ConvertViewToString("_CommunicationList", project, true) });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }

        private async Task<string> UploadDocument(string filePath, IFormFile file)
        {
            try
            {
                if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, filePath)))
                    Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, filePath));

                var uploadedFilePath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);

                return await UploadFile(uploadedFilePath, file);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }
        }

        public IActionResult DownloadDocument(string fileName)
        {
            var filePath = DocumentUploadFilePath.ProjectFilePath;
            var sPhysicalPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath + fileName);
            if (!System.IO.File.Exists(sPhysicalPath))
                return Content($"file not found.");

            return DownloadAnyFile(fileName, sPhysicalPath, null);
        }
        public async Task<PartialViewResult> RemoveDocument(int pdId, int pId, string fName)
        {
            try
            {
                var project = new Project();
                project.ProjectId = pId;
                var isDeleted = _projectService.DeleteDocument(pdId);
                if (isDeleted)
                {
                    var filePath = DocumentUploadFilePath.ProjectFilePath;
                    var sPhysicalPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath + fName);
                    if (System.IO.File.Exists(sPhysicalPath))
                    {
                        FileInfo myfile = new FileInfo(sPhysicalPath);
                        myfile.Delete();
                    }
                }
                project.ProjectDocument = await _projectService.GetDocumentList(project.ProjectId, (int)Common.ProcessDocument.DocumentProject);

                return PartialView("_DocumentList", project);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }

        #endregion

        #region Communication
        [HttpPost]
        public JsonResult ArchiveCommunication(int id, bool isChecked)
        {
            _logger.LogInformation("BlockController/ArchiveCommunication");
            var result = _projectService.ArchiveCommunication(id, isChecked);
            return Json(result);

        }

        public async Task<PartialViewResult> ShowArchive(int pId, bool isArchive)
        {
            try
            {
                var project = new Project();
                project.ProjectId = pId;
                project.ProjectCommunication = await _projectService.GetCommunications(project.ProjectId, isArchive);

                return PartialView("_CommunicationList", project);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message-" + ex.Message + " StackTrace-" + ex.StackTrace + " DatetimeStamp-" + DateTime.Now);
                throw;
            }

        }
        #endregion

    }

}
