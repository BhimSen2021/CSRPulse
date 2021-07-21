using AutoMapper;
using CSRPulse.Data.Repositories;
using CSRPulse.Model;
using DTOModel = CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CSRPulse.Services
{
    public class ProcessSetupServices : BaseService, IProcessSetupServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public ProcessSetupServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<bool> CreateProcessSetup(List<ProcessSetup> processes)
        {
            try
            {
                var processId = processes.Select(p => p.ProcessId).FirstOrDefault();
                var model = _mapper.Map<List<DTOModel.ProcessSetup>>(processes);

                var isExist = await _genericRepository.ExistsAsync<DTOModel.ProcessSetup>(x => x.ProcessId == processId);
                if (!isExist)
                {
                    return await _genericRepository.AddMultipleEntityAsync(model);
                }
                else
                {
                    var oldSetup = await _genericRepository.GetAsync<DTOModel.ProcessSetup>(x => x.ProcessId == processId);
                    if (oldSetup != null && oldSetup.ToList().Count > 0)
                    {
                        var hModel = _mapper.Map<List<DTOModel.ProcessSetupHistory>>(oldSetup);
                        await _genericRepository.AddMultipleEntityAsync<DTOModel.ProcessSetupHistory>(hModel);
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.ProcessSetup>(oldSetup);
                        return await _genericRepository.AddMultipleEntityAsync(model);
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProcessSetup>> GetProcessSetupById(int processId)
        {
            try
            {
                var processSetup = await _genericRepository.GetAsync<DTOModel.ProcessSetup>(x => x.ProcessId == processId);
                if (processSetup != null)
                    return _mapper.Map<List<ProcessSetup>>(processSetup);
                else
                    return new List<ProcessSetup>();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateProcessSetup(List<ProcessSetup> processes)
        {
            try
            {
                var processId = processes.Select(p => p.ProcessId).FirstOrDefault();
                var oldSetup = await _genericRepository.GetAsync<DTOModel.ProcessSetup>(x => x.ProcessId == processId);
                if (oldSetup != null)
                {
                    //var pId_rId = oldSetup.Select(x => new
                    //{
                    //    processId = x.ProcessId,
                    //    RevisionNo = x.RevisionNo ?? 1
                    //}).FirstOrDefault();

                    // Update history
                    //var oldSetupHistory = await _genericRepository.GetAsync<DTOModel.ProcessSetupHistory>(x => x.ProcessId == pId_rId.processId && x.RevisionNo == pId_rId.RevisionNo);
                    //oldSetupHistory.ToList().ForEach(h =>
                    //{
                    //    h.EndDate = DateTime.Now;
                    //});

                    var model = _mapper.Map<List<DTOModel.ProcessSetup>>(processes);
                    var hModel = _mapper.Map<List<DTOModel.ProcessSetupHistory>>(processes);

                    await _genericRepository.RemoveMultipleEntityAsync<DTOModel.ProcessSetup>(oldSetup);
                    await _genericRepository.AddMultipleEntityAsync<DTOModel.ProcessSetup>(model);
                    await _genericRepository.AddMultipleEntityAsync<DTOModel.ProcessSetupHistory>(hModel);

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> InsertProcessSetupHistory(List<ProcessSetupHistory> setupHistories)
        {
            try
            {
                var model = _mapper.Map<List<DTOModel.ProcessSetupHistory>>(setupHistories);

                return await _genericRepository.AddMultipleEntityAsync(model);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProcessSetupHistory>> GetProcessSetupHistoryBySetupId(int processId)
        {
            try
            {
                var history = await _genericRepository.GetByIDAsync<List<DTOModel.ProcessSetupHistory>>(processId);
                if (history != null)
                    return _mapper.Map<List<ProcessSetupHistory>>(history);
                else
                    return new List<ProcessSetupHistory>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
