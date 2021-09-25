using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSRPulse.Services
{
    public class DesignationHistoryService : BaseService, IDesignationHistoryService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IDesignationHistoryRepository _repository;
        private readonly IMapper _mapper;
        public DesignationHistoryService(IGenericRepository generic, IDesignationHistoryRepository repository, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> CreateDesignation(DesignationHistory designationHistory)
        {
            using (var transaction = _genericRepository.BeginTransaction())
            {
                try
                {
                    var model = _mapper.Map<DTOModel.DesignationHistory>(designationHistory);
                    var res = await _genericRepository.InsertAsync(model);

                    transaction.Commit();
                    if (res > 0)
                        return true;
                    else
                        return false;

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<DesignationHistory> GetDesignationHistoryById(int designationHistoryId)
        {
            try
            {
                var designationHistory = await _genericRepository.GetByIDAsync<DTOModel.DesignationHistory>(designationHistoryId);
                if (designationHistory != null)
                    return _mapper.Map<DesignationHistory>(designationHistory);
                else
                    return new DesignationHistory();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<DesignationHistory>> GetDesignationHistoryByUserId(int userId)
        {
            try
            {
                var designationHistory = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.DesignationHistory>(x => x.UserId == userId && x.IsDeleted == false).Include(s => s.Designation));

                if (designationHistory != null)
                    return _mapper.Map<List<DesignationHistory>>(designationHistory);
                else
                    return new List<DesignationHistory>();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> RevertDesignation(int hId, int uId)
        {
            var hData = await _genericRepository.GetByIDAsync<DTOModel.DesignationHistory>(hId);
            if (hData != null)
            {
                // Revert Designation 
                hData.IsDeleted = true;
                hData.Todate = DateTime.Now;
                _genericRepository.Update(hData);

                var hDataPrevious = await _repository.GetLastDesignationHistory(uId);
                if (hDataPrevious != null)
                {
                    // Update ToDate in Previous Designation
                    hDataPrevious.Todate = null;
                    _genericRepository.Update(hDataPrevious);

                    // Update Designation in user table
                    var uData = await _genericRepository.GetByIDAsync<DTOModel.User>(uId);
                    if (uData != null)
                    {
                        uData.DesignationId = hDataPrevious.DesignationId;
                        _genericRepository.Update(uData);
                    }
                }

                return true;
            }
            return false;

        }

        public async Task<bool> UpdateTodatePrevious(DesignationHistory designationHistory)
        {
            try
            {
                var hData = await _genericRepository.GetFirstOrDefaultAsync<DTOModel.DesignationHistory>(h => h.UserId == designationHistory.UserId && h.Todate == null);
                if (hData != null)
                {
                    hData.Todate = DateTime.Now;
                    hData.UpdatedOn = DateTime.Now;
                    hData.UpdatedBy = designationHistory.UpdatedBy;
                    hData.UpdatedRid = designationHistory.UpdatedRid;
                    hData.UpdatedRname = designationHistory.UpdatedRname;
                    _genericRepository.Update(hData);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
