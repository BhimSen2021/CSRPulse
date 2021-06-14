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
    public class VillageServices : BaseService, IVillageServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public VillageServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<bool> CreateVillage(Model.Village village)
        {
            try
            {
                var dtoVillage = _mapper.Map<DTOModel.Village>(village);

                var res = await _genericRepository.InsertAsync(dtoVillage);
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

        public async Task<List<Model.Village>> GetVillageList()
        {
            try
            {
                var getVillages = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.Village>().Include(s => s.State).Include(d => d.District).Include(b=> b.Block));
                var disList = _mapper.Map<List<Model.Village>>(getVillages);
                return disList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Model.Village> GetVillageById(int villageId)
        {
            try
            {
                var getVillages = await _genericRepository.GetByIDAsync<DTOModel.Village>(villageId);
                if (getVillages != null)
                    return _mapper.Map<Model.Village>(getVillages);
                else
                    return new Model.Village();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdateVillage(Model.Village village)
        {
            try
            {

                var getVillages = await _genericRepository.GetByIDAsync<DTOModel.Village>(village.VillageId);
                if (getVillages != null)
                {
                    if (getVillages.VillageName == village.VillageName && getVillages.VillageCode == village.VillageCode && getVillages.StateId == village.StateId && getVillages.DistrictId == village.DistrictId && getVillages.BlockId == village.BlockId)
                        return true;
                    //else
                    //{
                    //    var recordExist = _genericRepository.GetIQueryable<DTOModel.Village>(x => x.VillageName == village.VillageName || x.VillageShort == village.VillageShort || x.VillageCode == village.VillageCode).FirstOrDefault();

                    //    if (recordExist != null)
                    //    {
                    //        village.RecordExist = true;
                    //        return false;
                    //    }
                    //}
                    getVillages.VillageName = village.VillageName;
                    getVillages.VillageCode = village.VillageCode;
                    getVillages.StateId = village.StateId;
                    getVillages.DistrictId = village.DistrictId;
                    getVillages.BlockId = village.BlockId;
                    getVillages.LocationType = (int)village.LocationType;
                    getVillages.UpdatedBy = village.UpdatedBy;
                    getVillages.UpdatedOn = village.UpdatedOn;
                    _genericRepository.Update(getVillages);
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

        public async Task<bool> RecordExist(Model.Village village)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.Village>(x => x.VillageName == village.VillageName || x.VillageCode == village.VillageCode);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
