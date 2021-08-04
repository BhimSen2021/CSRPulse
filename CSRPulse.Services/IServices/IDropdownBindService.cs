using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services.IServices
{
    public interface IDropdownBindService
    {
        List<SelectListModel> GetCountryAsync(int? countryId);
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

        IEnumerable<SelectListModel> GetDesignations(int? designationId);

        IEnumerable<SelectListModel> GetPartners(int? partnerId);

        IEnumerable<SelectListModel> GetProcess(int? processId);
        IEnumerable<SelectListModel> GetModule(int? moduleId);
        IEnumerable<SelectListModel> GetFYYear(int startYear);
    }
}
