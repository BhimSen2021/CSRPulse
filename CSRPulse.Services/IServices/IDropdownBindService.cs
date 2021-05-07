using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Services.IServices
{
   public interface IDropdownBindService
    {
        List<SelectListModel> GetCountryAsync(int? countryId);
        List<SelectListModel> GetStateAsync(int? countryId,int ? stateId);
    }
}
