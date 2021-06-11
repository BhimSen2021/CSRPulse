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
        IEnumerable<SelectListModel> GetRole(int? roleid);
    }
}
