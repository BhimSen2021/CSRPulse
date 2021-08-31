using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CSRPulse.Services
{
    public class PartnerPolicyModuleService : BaseService, IPartnerPolicyModuleService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public PartnerPolicyModuleService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<PartnerPolicyModule> CreatePartnerPolicyModuleAsync(PartnerPolicyModule PartnerPolicyModule)
        {
            try
            {

                var model = _mapper.Map<DTOModel.PartnerPolicyModule>(PartnerPolicyModule);

                var IsExist = _genericRepository.Exists<DTOModel.PartnerPolicyModule>(x => x.PolicyModuleName.ToLower() == PartnerPolicyModule.PolicyModuleName.ToLower() && x.PolicyId == PartnerPolicyModule.PolicyId);
                if (!IsExist)
                {
                    var id = await _genericRepository.InsertAsync(model);
                    PartnerPolicyModule.PolicyId = id;
                }
                PartnerPolicyModule.IsExist = IsExist;
                return PartnerPolicyModule;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PartnerPolicyModule> GetPartnerPolicyModuleByIdAsync(int policyModuleId)
        {
            try
            {
                var result = await _genericRepository.GetByIDAsync<DTOModel.PartnerPolicyModule>(policyModuleId);
                if (result != null)
                    return _mapper.Map<PartnerPolicyModule>(result);
                else
                    return new PartnerPolicyModule();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PartnerPolicyModule>> GetPartnerPolicyModuleAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.PartnerPolicyModule>();
                return _mapper.Map<List<PartnerPolicyModule>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PartnerPolicyModule>> GetPartnerPolicyModuleAsync(PartnerPolicyModule partnerpolicymodule)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.PartnerPolicyModule>(x => x.PolicyId == partnerpolicymodule.PolicyId && (partnerpolicymodule.IsActiveFilter.HasValue ? x.IsActive == partnerpolicymodule.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<PartnerPolicyModule>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePartnerPolicyModuleAsync(PartnerPolicyModule theme)
        {
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.PartnerPolicyModule>(x => x.PolicyModuleName.ToLower() == theme.PolicyModuleName.ToLower() && x.PolicyModuleId != theme.PolicyModuleId && x.PolicyId != theme.PolicyId);

                theme.IsExist = IsExist;

                if (!IsExist)
                {
                    var result = await _genericRepository.GetByIDAsync<DTOModel.PartnerPolicyModule>(theme.PolicyModuleId);
                    if (result != null)
                    {
                        result.PolicyId = theme.PolicyId;
                        result.PolicyModuleName = theme.PolicyModuleName;
                        result.IsActive = theme.IsActive;
                        result.UpdatedOn = theme.UpdatedOn;
                        result.UpdatedBy = theme.UpdatedBy;
                        _genericRepository.Update(result);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }





        public async Task<bool> RecordExist(Model.PartnerPolicyModule PartnerPolicyModule)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.PartnerPolicyModule>(x => x.PolicyModuleName == PartnerPolicyModule.PolicyModuleName);
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
                var model = _genericRepository.GetByID<DTOModel.PartnerPolicyModule>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ActiveDeActiveIsFormallyApprovedByBoard(int id, bool IsFormallyApprovedByBoard)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.PartnerPolicyModule>(id);
                model.IsFormallyApprovedByBoard = IsFormallyApprovedByBoard;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ActiveDeActiveIsImplementedSince(int id, bool IsImplementedSince)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.PartnerPolicyModule>(id);
                model.IsImplementedSince = IsImplementedSince;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ActiveDeActiveIsLastUpdatedOn(int id, bool IsLastUpdatedOn)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.PartnerPolicyModule>(id);
                model.IsLastUpdatedOn = IsLastUpdatedOn;
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
