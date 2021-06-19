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

        public DropdownBindService(IGenericRepository generic, IMapper mapper)
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
            var dtoDist = _genericRepository.Get<DTOModel.State>(x => stateId.HasValue ? x.StateId == stateId : (1 > 0) && x.IsActive == true);
            var districtList = _mapper.Map<List<SelectListModel>>(dtoDist);
            return districtList.ToList();

        }

        public IEnumerable<SelectListModel> GetAllEmails()
        {
            var uEmails = _genericRepository.Get<DTOModel.User>(u => u.IsActive == true && u.IsDeleted == false).Select(x => new SelectListModel() { id = x.UserId, value = x.EmailId }).ToList();

            var cEmails = _genericRepository.Get<DTOModel.Customer>(c => c.IsDeleted == false).Select(x => new SelectListModel() { id = x.CustomerId, value = x.Email }).ToList();

            uEmails.AddRange(cEmails);

            return uEmails.OrderBy(x => x.value);
        }

        public IEnumerable<SelectListModel> GetRole(int? roleid)
        {
            var dtoRole = _genericRepository.Get<DTOModel.Role>(x => roleid.HasValue ? x.RoleId == roleid.Value : (1 > 0) && x.IsActive == true);
            var roleList = _mapper.Map<List<SelectListModel>>(dtoRole);
            return roleList.ToList();

        }

        public IEnumerable<SelectListModel> GetDistrict(int? stateId, int? districtId)
        {
            try
            {

                var dtoDistrict = _genericRepository.Get<DTOModel.District>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)) && (districtId.HasValue ? x.DistrictId == districtId.Value : (1 > 0)) && x.IsActive == true);
                var districtList = _mapper.Map<List<SelectListModel>>(dtoDistrict);
                return districtList.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<SelectListModel> GetBlock(int? stateId, int? districtId, int? blockId)
        {
            try
            {

                var dtoBlock = _genericRepository.Get<DTOModel.Block>(x => (stateId.HasValue ? x.StateId == stateId.Value : (1 > 0)) && (districtId.HasValue ? x.DistrictId == districtId.Value : (1 > 0)) && (blockId.HasValue ? x.BlockId == blockId.Value : (1 > 0)) && x.IsActive == true);
                var blockList = _mapper.Map<List<SelectListModel>>(dtoBlock);
                return blockList.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<SelectListModel> GetTheme(int? themeId)
        {
            try
            {
                var theme = _genericRepository.Get<DTOModel.Theme>(x => (themeId.HasValue ? x.ThemeId == themeId.Value : (1 > 0)) && x.IsActive == true);
                var themeList = _mapper.Map<List<SelectListModel>>(theme);
                return themeList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetSubTheme(int? themeId, int? subThemeId)
        {
            try
            {
                var subTheme = _genericRepository.Get<DTOModel.SubTheme>(x => (themeId.HasValue ? x.ThemeId == themeId.Value : (1 > 0)) && (subThemeId.HasValue ? x.SubThemeId == subThemeId.Value : (1 > 0)) && x.IsActive == true);
                var subThemeList = _mapper.Map<List<SelectListModel>>(subTheme);
                return subThemeList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetActivity(int? themeId, int? activityId)
        {
            try
            {
                var activity = _genericRepository.Get<DTOModel.Activity>(x => (themeId.HasValue ? x.ThemeId == themeId.Value : (1 > 0)) && (activityId.HasValue ? x.ActivityId == activityId.Value : (1 > 0)) && x.IsActive == true);

                var activityList = _mapper.Map<List<SelectListModel>>(activity);
                return activityList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetSubActivity(int? themeId, int? activityId)
        {
            try
            {
                var subActivity = _genericRepository.Get<DTOModel.SubActivity>(x => (themeId.HasValue ? x.ThemeId == themeId.Value : (1 > 0)) && (activityId.HasValue ? x.ActivityId == activityId.Value : (1 > 0)) && x.IsActive == true);

                var subActivityList = _mapper.Map<List<SelectListModel>>(subActivity);
                return subActivityList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetUoM(int? uomId)
        {
            try
            {
                var uoms = _genericRepository.Get<DTOModel.Uom>(x => (uomId.HasValue ? x.Uomid == uomId.Value : (1 > 0)) && x.IsActive == true);
                var uomList = _mapper.Map<List<SelectListModel>>(uoms);
                return uomList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetIndicatorResponseType()
        {
            try
            {
                var indicatorResponseTypes = _genericRepository.Get<DTOModel.IndicatorResponseType>(x => x.IsActive == true);
                var selectLists = _mapper.Map<List<SelectListModel>>(indicatorResponseTypes);
                return selectLists.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<SelectListModel> GetIndicatorType()
        {
            try
            {
                var indicatorTypes = _genericRepository.Get<DTOModel.IndicatorType>(x => x.IsActive == true);
                var selectLists = _mapper.Map<List<SelectListModel>>(indicatorTypes);
                return selectLists.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
