using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using CSRPulse.Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Services
{
    public class DropdownBindService : BaseService, IDropdownBindService
    {
        private readonly IGenericRepository _genericRepository;
        public DropdownBindService(IGenericRepository generic)
        {
            _genericRepository = generic;
        }
        public List<SelectListModel> GetCountryAsync(int? countryId)
        {
            throw new NotImplementedException();
        }

        public List<SelectListModel> GetStateAsync(int? countryId, int? stateId)
        {
            throw new NotImplementedException();
        }
    }
}
