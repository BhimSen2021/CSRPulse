using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IProjectService
    {
        bool ActiveDeActive(int id, bool IsActive);
        Task<Project> CreateStateAsync(Project project);
        Task<List<Project>> GetProjectAsync();
        Project GetProjectById(int projectId);
        Task<bool> UpdateProjectAsync(Project project);
        Task<List<ProjectInterventionReport>> GetInterventionReportAsync(int projectId);
        Task<List<ProjectLocationDetail>> GetTvLocationDetails(int projectId, int implementationLevel);
        List<ProjectLocationDetail> GetLocationDetails(int projectId, int lLevel);
        Task<bool> AddLocationDetails(List<ProjectLocationDetail> locationDetails, int projectId);
        Task<List<ProjectDocument>> GetDocumentList(int projectId, int processId);
        Task<int> SaveDocument(ProjectDocument document);
        bool DeleteDocument(int pdId);
        Task UpdateDocument(ProjectDocument document);

        Task<int> SaveCommunication(ProjectCommunication communication);
        Task<List<ProjectCommunication>> GetCommunications(int projectId, bool? IsActive);
        bool ArchiveCommunication(int id, bool IsActive);
    }
}