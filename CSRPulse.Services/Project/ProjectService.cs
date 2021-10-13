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
                var result = await _genericRepository.GetAsync<DTOModel.ProjectDocument>(x => x.ProjectId == projectId);
                if (result != null)
                {
                    return result.Select(d => new ProjectDocument()
                    {
                        ProjectDocumentId = d.ProjectDocumentId,
                        ProjectId = d.ProjectId,
                        DocumentId = d.DocumentId,
                        DocumentName = d.DocumentName,
                        UploadFileName = d.UploadFileName,
                        ServerFileName = d.ServerFileName,
                        DocumentMaxSize = d.DocumentMaxSize ?? 20,
                        DocumentType = d.DocumentType,
                        Mandatory = d.Mandatory ?? false,
                        Remark = d.Remark,
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
        public async Task<List<Model.Document>> GetProcessDocument(int processId)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.ProcessDocument>(x => x.ProcessId == processId && x.IsActive == true);
                if (result != null)
                {
                    return result.Select(d => new Model.Document()
                    {
                        DocumentId = d.DocumentId,
                        DocumentName = d.DocumentName,
                        DocumentMaxSize = d.DocumentMaxSize ?? 20,
                        DocumentType = ExtensionMethods.GetUploadDocumentType(d.DocumentType),
                        Mandatory = d.Mandatory ?? false,
                        Remark = d.Remark
                    }).ToList();
                }
                else
                    return new List<Model.Document>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> AddDocument(ProjectDocument projectDocument)
        {
            try
            {
                int flag = 1;
                var model = _mapper.Map<DTOModel.ProjectDocument>(projectDocument);
                if (!await _genericRepository.ExistsAsync<DTOModel.ProjectDocument>
                      (x => x.DocumentId == projectDocument.DocumentId && x.ProjectId == projectDocument.ProjectId))
                    await _genericRepository.InsertAsync<DTOModel.ProjectDocument>(model);
                else
                    flag = 2;

                return flag;
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
        public async Task<List<NarrativeQuestion>> GetNarrativeAsync(int? processId)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.NarrativeQuestion>(x => (processId.HasValue ? x.ProcessId == processId.Value : 1 > 0) && x.IsActive == true);
                return _mapper.Map<List<NarrativeQuestion>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> AddProjectNarrative(ProjectNarrativeQuestion projectNarrative)
        {
            try
            {
                int flag = 1;
                var model = _mapper.Map<DTOModel.ProjectNarrativeQuestion>(projectNarrative);
                if (!await _genericRepository.ExistsAsync<DTOModel.ProjectNarrativeQuestion>
                      (x => x.QuestionId == projectNarrative.QuestionId && x.ProjectId == projectNarrative.ProjectId))
                    await _genericRepository.InsertAsync<DTOModel.ProjectNarrativeQuestion>(model);
                else
                    flag = 2;

                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProjectNarrativeQuestion>> GetProjectNarrative(ProjectNarrativeQuestion projectNarrative)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.ProjectNarrativeQuestion>
                    (x => x.ProjectId == projectNarrative.ProjectId
                    && x.ProcessId == projectNarrative.ProcessId
                    && x.IsActive == true);
                return _mapper.Map<List<ProjectNarrativeQuestion>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProjectTeam>> GetTeamMemeber(int RoleId)
        {
            try
            {
                var result = await _projectRepository.GetTeamMemeber(RoleId).ToListAsync();
                if (result != null)
                {
                    return result.Select(d => new Model.ProjectTeam()
                    {
                        UserId = d.UserId,
                        RoleId = d.RoleId,
                        RoleName = d.RoleName,
                        FullName = d.FullName,
                        EmailID=ExtensionMethods.MakeDisplayEmail(d.EmailID),
                        //EmailID = d.EmailID,
                        MobileNo = d.MobileNo,
                        DesignationId = d.DesignationId,
                    }).ToList();
                }
                else
                    return new List<Model.ProjectTeam>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> AddTeamMember(ProjectTeamDetail projectTeamMember)
        {
            try
            {
                int flag = 1;
                var model = _mapper.Map<DTOModel.ProjectTeamDetail>(projectTeamMember);
                if (!await _genericRepository.ExistsAsync<DTOModel.ProjectTeamDetail>
                      (x => x.UserId == projectTeamMember.UserId && x.ProjectId == projectTeamMember.ProjectId && x.FromDate == null))
                    await _genericRepository.InsertAsync<DTOModel.ProjectTeamDetail>(model);
                else
                    flag = 2;

                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteTeamDetail(int projectId)
        {
            try
            {
                var model = _genericRepository.GetByID<DTOModel.ProjectTeamDetail>(projectId);
                model.FromDate = DateTime.Now;
                _genericRepository.Update(model);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProjectTeamDetail>> Getteammemberslist(int projectId, int roleId)
        {
            try
            {
                var result = await _projectRepository.Getteammemberslist(projectId, roleId).ToListAsync();
                if (result != null)
                {
                    return result.Select(l => new ProjectTeamDetail()
                    {
                        ProjectTeamDetailId = l.ProjectTeamDetailId,
                        RoleId = l.RoleId,
                        UserId = l.UserId,
                        FullName = l.FullName,
                        RoleName = l.RoleName,
                        MobileNo = l.MobileNo,
                        EmailId= ExtensionMethods.MakeDisplayEmail(l.EmailID),
                    //EmailId = l.EmailID,
                        ToDate = l.ToDate,
                        FromDate = l.FromDate
                    }).ToList();
                }
                else
                    return new List<ProjectTeamDetail>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProjectNarrativeQuestion>> GetProjectOverView(int projectId)
        {
            try
            {
                var result = await _genericRepository.GetAsync<DTOModel.ProjectNarrativeQuestion>(x => x.ProjectId == projectId);
                if (result != null)
                {
                    return result.Select(d => new ProjectNarrativeQuestion()
                    {
                        ProjectQuestionId = d.ProjectQuestionId,
                        ProjectId = d.ProjectId,
                        QuestionId = d.QuestionId,
                        Question = d.Question,
                        ProcessId = d.ProcessId,
                        CommentRequire = d.CommentRequire,
                        QuestionType = d.QuestionType,
                        ContentLimit = d.ContentLimit,
                        OrderNo = d.OrderNo,
                        CreatedBy = d.CreatedBy,
                        CreatedOn = d.CreatedOn,
                        CreatedRid = d.CreatedRid,
                        CreatedRname = d.CreatedRname
                    }).ToList();
                }
                else
                    return new List<ProjectNarrativeQuestion>();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<List<ProjectOverviewModule>> GetOverviewlist(int projectId)
        {
            try
            {
                var result = await _projectRepository.GetOverview(projectId).ToListAsync();
                if (result != null)
                {
                    return result.Select(l => new ProjectOverviewModule()
                    {
                        ProjectQuestionId = l.ProjectQuestionId,
                        ProjectId = l.ProjectId,
                        QuestionId = l.QuestionId,
                        Question = l.Question,
                        ProcessId = l.ProcessId,
                        CommentRequire = l.CommentRequire,
                        QuestionType = l.QuestionType,
                        ContentLimit = l.ContentLimit,
                        OrderNo = l.OrderNo,
                        Projectanswer = l.Projectanswer
                    }).ToList();
                }
                else
                    return new List<ProjectOverviewModule>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProjectNarrativeAnswer>> GetUpdateProjectOverView(Project project)
        {
            try
            {
                var model = _mapper.Map<List<DTOModel.ProjectNarrativeAnswer>>(project.ProjectOverviewModule);
                var isExist = await _genericRepository.ExistsAsync<DTOModel.ProjectNarrativeAnswer>(x => x.ProjectId == project.ProjectId);
                if (!isExist)
                {
                    await _genericRepository.AddMultipleEntityAsync(model);
                }
                else
                {
                    var oldDetails = await _genericRepository.GetAsync<DTOModel.ProjectNarrativeAnswer>(x => x.ProjectId == project.ProjectId);
                    if (oldDetails != null && oldDetails.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.ProjectNarrativeAnswer>(oldDetails);
                        await _genericRepository.AddMultipleEntityAsync(model);
                    }
                }
                var result = await _genericRepository.GetAsync<DTOModel.ProjectNarrativeAnswer>(x => x.ProjectId == project.ProjectId);

                return _mapper.Map<List<ProjectNarrativeAnswer>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}
