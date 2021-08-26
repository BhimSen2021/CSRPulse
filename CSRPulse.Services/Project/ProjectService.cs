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

namespace CSRPulse.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        public ProjectService(IGenericRepository generic, IMapper mapper)
        {
            _genericRepository = generic;
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
                var projects = _genericRepository.GetIQueryable<DTOModel.Project>(p => p.ProjectId == projectId).Include(i => i.ProjectInternalSource).Include(o => o.ProjectOtherSource).Include(l => l.ProjectLocation);
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
            try
            {
                var IsExist = _genericRepository.Exists<DTOModel.Project>(x => x.ProjectName.ToLower() == project.ProjectName.ToLower() && x.ProjectId != project.ProjectId);

                project.IsExist = IsExist;

                if (!IsExist)
                {
                    var model = _mapper.Map<DTOModel.Project>(project);
                    await _genericRepository.UpdateAsync(model);

                    // Update project other sources
                    var oldOS = await _genericRepository.GetAsync<DTOModel.ProjectOtherSource>(x => x.ProjectId == project.ProjectId && x.RevisionNo == project.ProjectOtherSource.Select(r => r.RevisionNo).FirstOrDefault());
                    if (oldOS != null && oldOS.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.ProjectOtherSource>(oldOS);
                        await _genericRepository.AddMultipleEntityAsync(model.ProjectOtherSource);
                    }

                    // Update project internal sources
                    var oldIS = await _genericRepository.GetAsync<DTOModel.ProjectInternalSource>(x => x.ProjectId == project.ProjectId && x.RevisionNo == project.ProjectInternalSource.Select(r => r.RevisionNo).FirstOrDefault());
                    if (oldIS != null && oldIS.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.ProjectInternalSource>(oldIS);
                        await _genericRepository.AddMultipleEntityAsync(model.ProjectInternalSource);
                    }

                    // Update project location
                    var oldLocations = await _genericRepository.GetAsync<DTOModel.ProjectLocation>(x => x.ProjectId == project.ProjectId);
                    if (oldLocations != null && oldLocations.ToList().Count > 0)
                    {
                        await _genericRepository.RemoveMultipleEntityAsync<DTOModel.ProjectLocation>(oldLocations);
                        await _genericRepository.AddMultipleEntityAsync(model.ProjectLocation);
                    }


                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
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

    }
}
