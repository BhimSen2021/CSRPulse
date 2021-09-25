using AutoMapper;
using CSRPulse.Data.Repositories;
using System;
using CSRPulse.Model;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DTOModel = CSRPulse.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CSRPulse.Common;

namespace CSRPulse.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IGenericRepository generic, IProjectRepository projectRepository, IMapper mapper)
        {
            _genericRepository = generic;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<Project> CreateStateAsync(Project project)
        {
            try
            {
                var model = _mapper.Map<DTOModel.Project>(project);
                project.IsExist = _genericRepository.Exists<DTOModel.Project>(x => x.ProjectName.ToLower() == project.ProjectName.ToLower());
                if (!project.IsExist)
                {
                    project.ProjectId = await _genericRepository.InsertAsync(model);
                }

                return project;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Project>> GetProjectAsync()
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.Project>();
                return _mapper.Map<List<Project>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Project GetProjectById(int projectId)
        {
            try
            {
                var projects = _genericRepository.GetIQueryable<DTOModel.Project>(p => p.ProjectId == projectId).Include(i => i.ProjectInternalSource).Include(o => o.ProjectOtherSource).Include(l => l.ProjectLocation).Include(r => r.ProjectInterventionReport).Include(f => f.ProjectFinancialReport).Include(ld => ld.ProjectLocationDetail).ThenInclude(s => s.State).Include(ld => ld.ProjectLocationDetail).ThenInclude(d => d.District).Include(ld => ld.ProjectLocationDetail).ThenInclude(b => b.Block).Include(c => c.ProjectCommunication);

                var project = _mapper.Map<IEnumerable<DTOModel.Project>, IEnumerable<Project>>(projects);

                return project.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateProjectAsync(Project project)
        {
            using (var transaction = _genericRepository.BeginTransaction())
            {
                try
                {
                    var IsExist = _genericRepository.Exists<DTOModel.Project>(x => x.ProjectName.ToLower() == project.ProjectName.ToLower() && x.ProjectId != project.ProjectId);

                    project.IsExist = IsExist;

                    if (!IsExist)
                    {                        
                        var model = _mapper.Map<DTOModel.Project>(project);

                        _genericRepository.Update(model);

                        // Update project other sources
                        var oldOS = await _genericRepository.GetAsync<DTOModel.ProjectOtherSource>(x => x.ProjectId == project.ProjectId && x.RevisionNo == project.ProjectOtherSource.Select(r => r.RevisionNo).FirstOrDefault());
                        if (oldOS != null && oldOS.ToList().Count > 0)
                        {
                            await _genericRepository.RemoveMultipleEntityAsync<DTOModel.ProjectOtherSource>(oldOS);
                            await _genericRepository.AddMultipleEntityAsync(model.ProjectOtherSource);
                        }
                        else
                            await _genericRepository.AddMultipleEntityAsync(model.ProjectOtherSource);

                        // Update project internal sources
                        var oldIS = await _genericRepository.GetAsync<DTOModel.ProjectInternalSource>(x => x.ProjectId == project.ProjectId && x.RevisionNo == project.ProjectInternalSource.Select(r => r.RevisionNo).FirstOrDefault());
                        if (oldIS != null && oldIS.ToList().Count > 0)
                        {
                            await _genericRepository.RemoveMultipleEntityAsync<DTOModel.ProjectInternalSource>(oldIS);
                            await _genericRepository.AddMultipleEntityAsync(model.ProjectInternalSource);
                        }
                        else
                            await _genericRepository.AddMultipleEntityAsync(model.ProjectInternalSource);

                        // Update project location
                        var oldLocations = await _genericRepository.GetAsync<DTOModel.ProjectLocation>(x => x.ProjectId == project.ProjectId);
                        if (oldLocations != null && oldLocations.ToList().Count > 0)
                        {
                            await _genericRepository.RemoveMultipleEntityAsync<DTOModel.ProjectLocation>(oldLocations);
                            await _genericRepository.AddMultipleEntityAsync(model.ProjectLocation);
                        }
                        transaction.Commit();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<List<ProjectInterventionReport>> GetInterventionReportAsync(int projectId)
        {
            try
            {
                var reports = await _genericRepository.GetAsync<DTOModel.ProjectInterventionReport>(p => p.ProjectId == projectId);
                return _mapper.Map<List<ProjectInterventionReport>>(reports);
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
                var model = _genericRepository.GetByID<DTOModel.Project>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ProjectLocationDetail>> GetTvLocationDetails(int projectId, int lLevel)
        {
            try
            {
                var result = await _projectRepository.GetTvLocationDetails(projectId, lLevel).ToListAsync();
                if (result != null)
                {
                    return result.Select(l => new ProjectLocationDetail()
                    {
                        StateId = l.StateId,
                        StateName = l.StateName,
                        DistrictId = l.DistrictId,
                        DistrictName = l.DistrictName,
                        BlockId = l.BlockId,
                        BlockName = l.BlockName,
                        VillageId = l.VillageId,
                        VillageName = l.VillageName
                    }).ToList();
                }
                else
                    return new List<ProjectLocationDetail>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> SaveDocument(ProjectDocument document)
        {
            try
            {
                var model = _mapper.Map<DTOModel.ProjectDocument>(document);
                return await _genericRepository.InsertAsync(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteDocument(int pdId)
        {
            try
            {
                _genericRepository.Delete<DTOModel.ProjectDocument>(pdId);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpdateDocument(ProjectDocument document)
        {
            try
            {
                var model = _mapper.Map<DTOModel.ProjectDocument>(document);
                await _genericRepository.UpdateAsync(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProjectDocument>> GetDocumentList(int projectId, int processId)
        {
            try
            {
                var result = await _projectRepository.GetDocuments(projectId, processId).ToListAsync();
                if (result != null)
                {
                    return result.Select(d => new ProjectDocument()
                    {
                        ProjectDocumentId = d.ProjectDocumentId,
                        ProjectId = d.ProjectId,
                        DocumentId = d.DocumentId,
                        DocumentName = d.DocumentName,
                        MDocumentName = d.MDocumentName,
                        DocumentMaxSize = d.DocumentMaxSize,
                        DocumentType = ExtensionMethods.GetUploadDocumentType(d.DocumentType),
                        Mandatory = d.Mandatory,
                        ServerDocumentName = d.ServerDocumentName,
                        CreatedBy = d.CreatedBy,
                        CreatedOn = d.CreatedOn,
                        CreatedRid = d.CreatedRid,
                        CreatedRname = d.CreatedRname
                    }).ToList();
                }
                else
                    return new List<ProjectDocument>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ProjectLocationDetail> GetLocationDetails(int projectId, int lLevel)
        {
            try
            {
                var locationDetails = _genericRepository.GetIQueryable<DTOModel.ProjectLocationDetail>(p => p.ProjectId == projectId).Include(s => s.State).Include(d => d.District).Include(b => b.Block).Include(v => v.Village);

                return _mapper.Map<List<ProjectLocationDetail>>(locationDetails);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> SaveCommunication(ProjectCommunication communication)
        {
            try
            {
                var model = _mapper.Map<DTOModel.ProjectCommunication>(communication);
                return await _genericRepository.InsertAsync(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProjectCommunication>> GetCommunications(int projectId, bool? IsActive)
        {
            try
            {
                var communications = await Task.FromResult(_genericRepository.GetIQueryable<DTOModel.ProjectCommunication>(x => x.ProjectId == projectId && (IsActive.HasValue ? x.IsActive == IsActive : (1 > 0))));

                return _mapper.Map<List<ProjectCommunication>>(communications);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ArchiveCommunication(int id, bool IsActive)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.ProjectCommunication>(id);
                model.IsActive = IsActive;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddLocationDetails(List<ProjectLocationDetail> locationDetails, int projectId)
        {
            try
            {
                var projectLocationDetail = _mapper.Map<List<DTOModel.ProjectLocationDetail>>(locationDetails);

                var oldLocations = await _genericRepository.GetAsync<DTOModel.ProjectLocationDetail>(x => x.ProjectId == projectId);
                if (oldLocations != null && oldLocations.ToList().Count > 0)
                {
                    await _genericRepository.RemoveMultipleEntityAsync<DTOModel.ProjectLocationDetail>(oldLocations);
                    await _genericRepository.AddMultipleEntityAsync(projectLocationDetail);
                }
                else
                {
                    await _genericRepository.AddMultipleEntityAsync(projectLocationDetail);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
