using System.Linq;

namespace CSRPulse.Data.Repositories
{
    public interface IProjectRepository
    {
        IQueryable<ProjectRepository.ProjectLocationDetail> GetTvLocationDetails(int projectId, int? locationLavel = 2);        
    }
}