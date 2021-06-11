using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using CSRPulse.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
namespace CSRPulse.Services
{
    public class DropdownBindService : IDropdownBindService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public DropdownBindService(IGenericRepository generic,IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public List<SelectListModel> GetCountryAsync(int? countryId)
        {
            var countryList = new List<SelectListModel>()
                {
                    new SelectListModel { id = 1, value = "INDIA" }
                };
            return countryList;
        }

        public IEnumerable<SelectListModel> GetStateAsync(int? countryId, int? stateId)
        {
            var dtoDist = _genericRepository.Get<DTOModel.State>(x => stateId.HasValue ? x.StateId == stateId : (1 > 0));
            var districtList=  _mapper.Map<List<SelectListModel>>(dtoDist);
            return districtList.ToList();

        }

        public IEnumerable<SelectListModel> GetRole(int? roleid)
        {
            var dtoRole = _genericRepository.Get<DTOModel.Role>(x => roleid.HasValue ? x.RoleId == roleid.Value : (1 > 0));
            var roleList = _mapper.Map<List<SelectListModel>>(dtoRole);
            return roleList.ToList();

        }
    }
}
