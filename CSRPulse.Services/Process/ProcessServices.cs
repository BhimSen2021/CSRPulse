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
    public class ProcessServices : BaseService, IProcessServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        public ProcessServices(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
            _mapper = mapper;
        }

        public async Task<bool> CreateProcess(Process process)
        {
            try
            {
                var model = _mapper.Map<DTOModel.Process>(process);

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

        public async Task<Process> GetProcessById(int ProcessId)
        {
            try
            {
                var designation = await _genericRepository.GetByIDAsync<DTOModel.Process>(ProcessId);
                if (designation != null)
                    return _mapper.Map<Process>(designation);
                else
                    return new Process();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateProcess(Process process)
        {
            try
            {
                var dData = await _genericRepository.GetByIDAsync<DTOModel.Process>(process.ProcessName);
                if (dData != null)
                {
                    if (dData.ProcessName == process.ProcessName)
                        return true;

                    dData.ProcessName = process.ProcessName;
                    dData.Document = process.Document;

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

        public async Task<bool> RecordExist(Process process)
        {
            try
            {
                return await _genericRepository.ExistsAsync<DTOModel.Process>(x => x.ProcessName == process.ProcessName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Process>> GetProcessAsync(Process process)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Process>(x => (process.IsActiveFilter.HasValue ? x.IsActive == process.IsActiveFilter.Value : 1 > 0));
                return _mapper.Map<List<Process>>(result);
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
                var model = _genericRepository.GetByID<DTOModel.Process>(id);
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
