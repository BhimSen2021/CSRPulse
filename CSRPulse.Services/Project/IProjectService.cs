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
        Task<List<Document>> GetProcessDocument(int processId);
        Task<int> AddDocument(ProjectDocument projectDocument);
        Task<int> SaveDocument(ProjectDocument document);
        bool DeleteDocument(int pdId);
        Task UpdateDocument(ProjectDocument document);
        Task<int> SaveCommunication(ProjectCommunication communication);
        Task<List<ProjectCommunication>> GetCommunications(int projectId, bool? IsActive);
        bool ArchiveCommunication(int id, bool IsActive);
        Task<List<NarrativeQuestion>> GetNarrativeAsync(int? processId);
        Task<int> AddProjectNarrative(ProjectNarrativeQuestion projectNarrative);
        Task<List<ProjectNarrativeQuestion>> GetProjectNarrative(ProjectNarrativeQuestion projectNarrative);
        Task<List<ProjectTeam>> GetTeamMemeber(int RoleId);
        Task<int> AddTeamMember(ProjectTeamDetail projectTeamMember);
        Task<List<ProjectTeamDetail>> Getteammemberslist(int projectId, int roleId);
        bool DeleteTeamDetail(int partnerId);
        Task<List<ProjectNarrativeQuestion>> GetProjectOverView(int projectId);
        Task<List<ProjectOverviewModule>> GetOverviewlist(int projectId);
        Task<List<ProjectNarrativeAnswer>> GetUpdateProjectOverView(Project project);

    }
}