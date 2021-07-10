using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public class DesignationServices : BaseService, IDesignationServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public DesignationServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<bool> CreateDesignation(Designation designation)
        {
            try
            {
                var model = _mapper.Map<DTOModel.Designation>(designation);
                var res = await _genericRepository.InsertAsync(model);
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

        public async Task<Designation> GetDesignationById(int DesignationId)
        {
            try
            {
                var designation = await _genericRepository.GetByIDAsync<DTOModel.Designation>(DesignationId);
                if (designation != null)
                    return _mapper.Map<Designation>(designation);
                else
                    return new Designation();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateDesignation(Designation designation)
        {
            try
            {
                var dData = await _genericRepository.GetByIDAsync<DTOModel.Designation>(designation.DesignationId);
                if (dData != null)
                {
                    if (dData.DesignationName == designation.DesignationName)
                        return true;

                    dData.DesignationName = designation.DesignationName;
                    dData.UpdatedBy = designation.UpdatedBy;
                    dData.UpdatedOn = designation.UpdatedOn;
                    _genericRepository.Update(dData);
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

        public async Task<bool> RecordExist(Designation designation)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.Designation>(x => x.DesignationName == designation.DesignationName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Designation>> GetDesignationsAsync(Designation designation)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Designation>(x => (designation.IsActiveFilter.HasValue ? x.IsActive == designation.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<Designation>>(result);
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
                var model = _genericRepository.GetByID<DTOModel.Designation>(id);
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
