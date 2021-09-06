using AutoMapper;
using CSRPulse.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;

namespace CSRPulse.Services
{
    public class PartnerPolicyService : BaseService, IPartnerPolicyService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public PartnerPolicyService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<bool> CreatePartnerPolicy(Model.PartnerPolicy partnerPolicy)
        {
            try
            {
                var dtoPartnerPolicy = _mapper.Map<DTOModel.PartnerPolicy>(partnerPolicy);

                var res = await _genericRepository.InsertAsync(dtoPartnerPolicy);
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

        public async Task<List<Model.PartnerPolicy>> GetPartnerPolicyList()
        {
            try
            {
                var getPartnerPolicies = await _genericRepository.GetAsync<DTOModel.PartnerPolicy>();
                var partnerPolicyList = _mapper.Map<List<Model.PartnerPolicy>>(getPartnerPolicies);
                return partnerPolicyList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Model.PartnerPolicy> GetPartnerPolicyById(int policyId)
        {
            try
            {
                var getPartnerPolicies = await _genericRepository.GetByIDAsync<DTOModel.PartnerPolicy>(policyId);
                if (getPartnerPolicies != null)
                    return _mapper.Map<Model.PartnerPolicy>(getPartnerPolicies);
                else
                    return new Model.PartnerPolicy();

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> UpdatePartnerPolicy(Model.PartnerPolicy partnerPolicy)
        {
            try
            {

                var getPartnerPolicies = await _genericRepository.GetByIDAsync<DTOModel.PartnerPolicy>(partnerPolicy.PolicyId);
                if (getPartnerPolicies != null)
                {
                    if (getPartnerPolicies.PolicyName == partnerPolicy.PolicyName)
                        return true;

                    getPartnerPolicies.PolicyName = partnerPolicy.PolicyName;
                    getPartnerPolicies.IsLastUpdatedOn = partnerPolicy.IsLastUpdatedOn;
                    getPartnerPolicies.IsFormallyApprovedByBoard = partnerPolicy.IsFormallyApprovedByBoard;
                    getPartnerPolicies.IsImplementedSince = partnerPolicy.IsImplementedSince;
                    getPartnerPolicies.UpdatedBy = partnerPolicy.UpdatedBy;
                    getPartnerPolicies.UpdatedOn = partnerPolicy.UpdatedOn;
                    _genericRepository.Update(getPartnerPolicies);
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

        public async Task<bool> RecordExist(Model.PartnerPolicy partnerPolicy)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.PartnerPolicy>(x => x.PolicyName == partnerPolicy.PolicyName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Model.PartnerPolicy>> GetPartnerPolicyAsync(Model.PartnerPolicy partnerPolicy)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.PartnerPolicy>(x => (partnerPolicy.IsActiveFilter.HasValue ? x.IsActive == partnerPolicy.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<Model.PartnerPolicy>>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActiveDeActive(int id, bool IsActive)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.PartnerPolicy>(id);
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
                var model = _genericRepository.GetByID<DTOModel.PartnerPolicy>(id);
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
                var model = _genericRepository.GetByID<DTOModel.PartnerPolicy>(id);
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
                var model = _genericRepository.GetByID<DTOModel.PartnerPolicy>(id);
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
