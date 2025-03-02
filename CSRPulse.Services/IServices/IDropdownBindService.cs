﻿using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services.IServices
{
    public interface IDropdownBindService
    {
        List<SelectListModel> GetCountryAsync(int? countryId);
        List<SelectListModel> GetFundingSourceAsync(int? SourceType);
        List<SelectListModel> GetAuditorChecker(int? AuditCheckerId);
        List<SelectListModel> GetAuditorMaker(int? AuditMakerId);
        IEnumerable<SelectListModel> GetProjects(int? projectId);
        IEnumerable<SelectListModel> GetAuditors(int? auditorId);
        IEnumerable<SelectListModel> GetProgramManagers(int? ProgramManagerId);
        IEnumerable<SelectListModel> GetStateAsync(int? countryId, int? stateId);
        IEnumerable<SelectListModel> GetAllEmails();
        IEnumerable<SelectListModel> GetRole(int? roleid);

        IEnumerable<SelectListModel> GetDistrict(int? stateId, int? districtId);
        IEnumerable<SelectListModel> GetBlock(int? stateId, int? districtId, int? blockId);
        IEnumerable<SelectListModel> GetTheme(int? themeId);
        IEnumerable<SelectListModel> GetSubTheme(int? themeId, int? subThemeId);

        IEnumerable<SelectListModel> GetActivity(int? themeId, int? activityId);
        IEnumerable<SelectListModel> GetSubActivity(int? themeId, int? activityId);
        IEnumerable<SelectListModel> GetUoM(int? uomId);
        IEnumerable<SelectListModel> GetIndicatorResponseType();
        IEnumerable<SelectListModel> GetIndicatorType();
        IEnumerable<SelectListModel> GetDepartments();
        IEnumerable<SelectListModel> GetPartnerPolicy();

        IEnumerable<SelectListModel> GetDesignations(int? designationId);

        IEnumerable<SelectListModel> GetPartners(int? partnerId);

        IEnumerable<SelectListModel> GetProcess(int? processId);
        IEnumerable<SelectListModel> GetProcessByFinalStatus(int? finalStatus);
        IEnumerable<SelectListModel> GetModule(int? moduleId);
        IEnumerable<SelectListModel> GetFYYear(int startYear);
        IEnumerable<SelectListModel> GetFundingAgency(int? agencyType);
        IEnumerable<SelectListModel> GetSource(int? type);
        IEnumerable<SelectListModel> GetUsers(int? roleId);
        Task<IEnumerable<ProjectLocationDetail>> GetStateLocationAsync(int? stateId);
        Task<IEnumerable<ProjectLocationDetail>> GetDistrictLocationAsync(int? stateId, int? districtId);
    }
}
