using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;

namespace CSRPulse.Services
{
    public class AuditReviewParamterServices : BaseService, IAuditReviewParamterServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public AuditReviewParamterServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }

        public async Task<bool> CreateParamterAsync(AuditReviewParamter paramter)
        {
            try
            {
                var auditReviewParamter = _mapper.Map<DTOModel.AuditReviewParamter>(paramter);

                var res = await _genericRepository.InsertAsync(auditReviewParamter);
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

        public async Task<AuditReviewParamter> GetParamterByIdAsync(int paramterId)
        {
            try
            {
                var paramter = await _genericRepository.GetByIDAsync<DTOModel.AuditReviewParamter>(paramterId);
                if (paramter != null)
                    return _mapper.Map<AuditReviewParamter>(paramter);
                else
                    return new AuditReviewParamter();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateParamterAsync(AuditReviewParamter paramter)
        {
            try
            {

                var auditReviewParamter = await _genericRepository.GetByIDAsync<DTOModel.AuditReviewParamter>(paramter.ParamterId);
                if (auditReviewParamter != null)
                {
                    auditReviewParamter.ModuleId = paramter.ModuleId;
                    auditReviewParamter.Scope = paramter.Scope;
                    auditReviewParamter.ReviewInstruction = paramter.ReviewInstruction;
                    auditReviewParamter.Critical = paramter.Critical;
                    auditReviewParamter.Sequence = paramter.Sequence;
                    auditReviewParamter.ReferenceNo = paramter.ReferenceNo;
                    auditReviewParamter.MaximumMarks = paramter.MaximumMarks;
                    auditReviewParamter.IsActive = paramter.IsActive;
                    auditReviewParamter.UpdatedBy = paramter.UpdatedBy;
                    auditReviewParamter.UpdatedOn = paramter.UpdatedOn;
                    _genericRepository.Update(auditReviewParamter);
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

        public async Task<bool> RecordExistAsync(AuditReviewParamter paramter)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.AuditReviewParamter>(x => x.ModuleId == paramter.ModuleId && x.Scope == paramter.Scope);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AuditReviewParamter>> GetParamterAsync(AuditReviewParamter paramter)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.AuditReviewParamter>(x => (paramter.IsActiveFilter.HasValue ? x.IsActive == paramter.IsActiveFilter.Value : 1 > 0) && x.ModuleId ==paramter.ModuleId);
                return _mapper.Map<List<AuditReviewParamter>>(result);
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
                var model = _genericRepository.GetByID<DTOModel.AuditReviewParamter>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> Count()
        {
            return await _genericRepository.Count<DTOModel.AuditReviewParamter>();
        }
    }
}
