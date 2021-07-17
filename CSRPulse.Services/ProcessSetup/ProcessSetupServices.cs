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
    public class ProcessSetupServices : BaseService, IProcessSetupServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public ProcessSetupServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }
        public async Task<bool> CreateProcessSetup(ProcessSetup process)
        {
            try
            {
                var model = _mapper.Map<DTOModel.ProcessSetup>(process);
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
        public async Task<ProcessSetup> GetProcessSetupById(int setupId)
        {
            try
            {
                var processSetup = await _genericRepository.GetByIDAsync<DTOModel.ProcessSetup>(setupId);
                if (processSetup != null)
                    return _mapper.Map<ProcessSetup>(processSetup);
                else
                    return new ProcessSetup();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateProcessSetup(ProcessSetup processSetup)
        {
            try
            {
                var pData = await _genericRepository.GetByIDAsync<DTOModel.ProcessSetup>(processSetup.SetupId);
                if (pData != null)
                {
                    pData.ProcessId = processSetup.ProcessId;
                    pData.RevisionNo = processSetup.RevisionNo;
                    pData.PrimaryRoleId = processSetup.PrimaryRoleId;
                    pData.SecondoryRoleId = processSetup.SecondoryRoleId;
                    pData.TertiaryRoleId = processSetup.TertiaryRoleId;
                    pData.QuaternaryRoleId = processSetup.QuaternaryRoleId;
                    pData.LevelId = processSetup.LevelId;
                    pData.Sequence = processSetup.Sequence;
                    pData.Skip = processSetup.Skip;
                    pData.Updatedby = processSetup.UpdatedBy;
                    pData.UpdatedOn = processSetup.UpdatedOn;

                    _genericRepository.Update(pData);
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

        public async Task<bool> InsertProcessSetupHistory(ProcessSetupHistory setupHistory)
        {
            try
            {
                var model = _mapper.Map<DTOModel.ProcessSetupHistory>(setupHistory);
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

        public async Task<ProcessSetupHistory> GetProcessSetupHistoryBySetupId(int setupId)
        {
            try
            {
                var history = await _genericRepository.GetByIDAsync<DTOModel.ProcessSetupHistory>(setupId);
                if (history != null)
                    return _mapper.Map<ProcessSetupHistory>(history);
                else
                    return new ProcessSetupHistory();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
