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
    public class ProcessSetupServices : BaseService, IProcessSetupServices
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IProcessSetupRepository _processSetupRepository;
        private readonly IMapper _mapper;
        public ProcessSetupServices(IGenericRepository generic, IProcessSetupRepository processSetupRepository, IMapper mapper)
        {
            _genericRepository = generic;
            _processSetupRepository = processSetupRepository;
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
                        
                        var cdate = DateTime.Now;
                        hModel.ToList().ForEach(h =>
                        {
                            h.StartDate = h.CreatedOn;
                            h.EndDate = cdate;
                            h.CreatedOn = cdate;
                            h.CreatedBy = processes.Select(x => x.CreatedBy).FirstOrDefault();
                            h.CreatedRid = processes.Select(x => x.CreatedRid).FirstOrDefault();
                            h.CreatedRname = processes.Select(x => x.CreatedRname).FirstOrDefault();
                        });

                        await _genericRepository.AddMultipleEntityAsync<DTOModel.ProcessSetupHistory>(hModel);
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.ProcessSetup>(oldSetup);

                        model.ToList().ForEach(h => { h.SetupId = 0; h.RevisionNo = (h.RevisionNo + 1); });

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
                var models = await _processSetupRepository.GetProcessSetupById(processId).OrderBy(s => s.Sequence).ToListAsync();

                if (models != null)
                {
                    return models.Select(a => new ProcessSetup()
                    {
                        SetupId = a.SetupId,
                        ProcessId = a.ProcessId,
                        RevisionNo = a.RevisionNo ?? 1,
                        PrimaryRoleId = a.PrimaryRoleId ?? 1,
                        PrimaryRole = a.PrimaryRole,
                        SecondoryRoleId = a.SecondoryRoleId,
                        SecondoryRole = a.SecondoryRole,
                        TertiaryRoleId = a.TertiaryRoleId,
                        TertiaryRole = a.TertiaryRole,
                        QuaternaryRoleId = a.QuaternaryRoleId,
                        QuaternaryRole = a.QuaternaryRole,
                        LevelId = a.LevelId,
                        Skip = a.Skip,
                        Sequence = a.Sequence ?? 1

                    }).ToList();
                }

                else
                    return new List<ProcessSetup>();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateProcessSetup(ProcessSetup processes)
        {
            try
            {
                var model = await _genericRepository.GetByIDAsync<DTOModel.ProcessSetup>(processes.SetupId);
                if (model != null)
                {
                    model.Skip = processes.Skip;
                    model.UpdatedOn = processes.UpdatedOn;
                    model.Updatedby = processes.UpdatedBy;
                    model.UpdatedRid = processes.UpdatedRid;
                    model.UpdatedRname = processes.UpdatedRname;
                    _genericRepository.Update<DTOModel.ProcessSetup>(model);
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

        public async Task<List<ProcessSetupHistory>> GetPSHistoryDetails(int processId, int revisionNo)
        {
            try
            {
                var models = await _processSetupRepository.GetPSHistoryDetails(processId, revisionNo).OrderBy(s => s.Sequence).ToListAsync();

                if (models != null)
                {
                    return models.Select(a => new ProcessSetupHistory()
                    {
                        ProcessId = a.ProcessId,
                        RevisionNo = a.RevisionNo ?? 1,
                        PrimaryRoleId = a.PrimaryRoleId ?? 0,
                        PrimaryRole = a.PrimaryRole,
                        SecondoryRoleId = a.SecondoryRoleId,
                        SecondoryRole = a.SecondoryRole,
                        TertiaryRoleId = a.TertiaryRoleId,
                        TertiaryRole = a.TertiaryRole,
                        QuaternaryRoleId = a.QuaternaryRoleId,
                        QuaternaryRole = a.QuaternaryRole,
                        LevelId = a.LevelId,
                        Skip = a.Skip,
                        Sequence = a.Sequence ?? 1

                    }).ToList();
                }

                else
                    return new List<ProcessSetupHistory>();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SelectListModel>> GetRevisionHistoryDropdown(int processId)
        {
            var res = await _processSetupRepository.GetRevisionHistoryDropdown(processId).OrderByDescending(r => r.id).ToListAsync();
            return res.Select(s => new SelectListModel()
            {
                id = s.id,
                value = s.value
            }).ToList();
        }
    }

}