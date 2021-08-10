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
    public class AuditReviewModuleServices : BaseService, IAuditReviewModuleServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public AuditReviewModuleServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }

        public async Task<bool> CreateModuleAsync(AuditReviewModule module)
        {
            try
            {
                var auditReviewModule = _mapper.Map<DTOModel.AuditReviewModule>(module);

                var res = await _genericRepository.InsertAsync(auditReviewModule);
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

        public async Task<AuditReviewModule> GetModuleByIdAsync(int moduleId)
        {
            try
            {
                var module = await _genericRepository.GetByIDAsync<DTOModel.AuditReviewModule>(moduleId);
                if (module != null)
                    return _mapper.Map<AuditReviewModule>(module);
                else
                    return new AuditReviewModule();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateModuleAsync(AuditReviewModule module)
        {
            try
            {

                var auditReviewModule = await _genericRepository.GetByIDAsync<DTOModel.AuditReviewModule>(module.ModuleId);
                if (auditReviewModule != null)
                {
                    if (auditReviewModule.Module == module.Module && auditReviewModule.IsActive == module.IsActive && auditReviewModule.Sequence == module.Sequence)
                        return true;

                    auditReviewModule.Module = module.Module;
                    auditReviewModule.IsActive = module.IsActive;
                    auditReviewModule.Sequence = module.Sequence;
                    auditReviewModule.UpdatedBy = module.UpdatedBy;
                    auditReviewModule.UpdatedOn = module.UpdatedOn;
                    _genericRepository.Update(auditReviewModule);
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

        public async Task<bool> RecordExistAsync(AuditReviewModule module)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.AuditReviewModule>(x => x.Module == module.Module);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AuditReviewModule>> GetModuleAsync(AuditReviewModule module)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.AuditReviewModule>(x => (module.IsActiveFilter.HasValue ? x.IsActive == module.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<AuditReviewModule>>(result);
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
                var model = _genericRepository.GetByID<DTOModel.AuditReviewModule>(id);
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
            return await _genericRepository.Count<DTOModel.AuditReviewModule>();
        }
    }
}
