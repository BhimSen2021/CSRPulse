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
    }
}
