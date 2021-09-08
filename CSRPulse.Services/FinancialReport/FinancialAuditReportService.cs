using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Data.Repositories;
using AutoMapper;
using DTOModel = CSRPulse.Data.Models;


namespace CSRPulse.Services
{
    public class FinancialAuditReportService : BaseService, IFinancialAuditReportService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public FinancialAuditReportService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }

        public bool ActiveDeActive(int id, bool IsActive)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.FinancialAuditReport>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<FinancialAuditReport> CreateFinancialAsync(FinancialAuditReport financial)
        {
            try
            {
                var model = _mapper.Map<DTOModel.FinancialAuditReport>(financial);

                var IsExist = _genericRepository.Exists<DTOModel.FinancialAuditReport>(x => x.Ngoid == financial.NGOId);
                if (!IsExist)
                {
                    var id = await _genericRepository.InsertAsync(model);
                    financial.FareportId = id;
                }
                financial.IsExist = IsExist;
                return financial;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FinancialAuditReport>> GetFinancialAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.FinancialAuditReport>();
                return _mapper.Map<List<FinancialAuditReport>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FinancialAuditReport>> GetFinancialAsync(FinancialAuditReport financial)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.FinancialAuditReport>(x => (financial.IsActiveFilter.HasValue ? x.IsActive == financial.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<FinancialAuditReport>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FinancialAuditReport> GetFinancialByIdAsync(int fareportId)
        {
            try
            {
                var result = await _genericRepository.GetByIDAsync<DTOModel.FinancialAuditReport>(fareportId);
                if (result != null)
                    return _mapper.Map<FinancialAuditReport>(result);
                else
                    return new FinancialAuditReport();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateFinancialAsync(FinancialAuditReport financial)
        {
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.FinancialAuditReport>(x => x.Ngoid == financial.NGOId && x.FareportId != financial.FareportId);

                financial.IsExist = IsExist;

                if (!IsExist)
                {
                    var result = await _genericRepository.GetByIDAsync<DTOModel.FinancialAuditReport>(financial.FareportId);
                    if (result != null)
                    {
                        result.Ngoid = financial.NGOId;
                        result.ProjectId = financial.ProjectId;
                        result.AuditorId = financial.AuditorId;
                        result.AuditCheckerId = financial.AuditCheckerId;
                        result.AuditMakerId = financial.AuditMakerId;
                        result.ProgramManagerId = financial.ProgramManagerId;
                        result.AuditDate = financial.AuditDate;
                        result.IsActive =  financial.IsActive;
                        result.UpdatedOn = financial.UpdatedOn;
                        result.UpdatedBy = financial.UpdatedBy;
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
    }
}
