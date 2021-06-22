using AutoMapper;
using CSRPulse.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
namespace CSRPulse.Services
{
    public class DistrictServices : BaseService, IDistrictServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public DistrictServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<bool> CreateDistrict(Model.District district)
        {
            try
            {
                var dtoDistrict = _mapper.Map<DTOModel.District>(district);

                var res = await _genericRepository.InsertAsync(dtoDistrict);
                if (res > 0)
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Model.District>> GetDistrictList(Model.District district)
        {
            try
            {
                var getDistricts = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.District>(x => x.StateId == district.StateId && x.IsActive == district.IsActive).Include(s => s.State));
                var disList = _mapper.Map<List<Model.District>>(getDistricts);
                return disList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Model.District> GetDistrictById(int districtId)
        {
            try
            {
                var getDistricts = await _genericRepository.GetByIDAsync<DTOModel.District>(districtId);
                if (getDistricts != null)
                    return _mapper.Map<Model.District>(getDistricts);
                else
                    return new Model.District();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdateDistrict(Model.District district)
        {
            try
            {

                var getDistricts = await _genericRepository.GetByIDAsync<DTOModel.District>(district.DistrictId);
                if (getDistricts != null)
                {
                    if (getDistricts.DistrictName == district.DistrictName && getDistricts.DistrictCode == district.DistrictCode && getDistricts.DistrictShort == district.DistrictShort && getDistricts.StateId == district.StateId)
                        return true;

                    getDistricts.DistrictName = district.DistrictName;
                    getDistricts.DistrictShort = district.DistrictShort;
                    getDistricts.DistrictCode = district.DistrictCode;
                    getDistricts.StateId = district.StateId.Value;
                    getDistricts.UpdatedBy = district.UpdatedBy;
                    getDistricts.UpdatedOn = district.UpdatedOn;
                    _genericRepository.Update(getDistricts);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RecordExist(Model.District district)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.District>(x => x.DistrictName == district.DistrictName || x.DistrictCode == district.DistrictCode);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ActiveDeActive(int id, bool IsActive)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.District>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
