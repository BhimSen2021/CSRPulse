using System.Linq;

namespace CSRPulse.Data.Repositories
{
    public interface IProcessSetupRepository
    {
        IQueryable<ProcessSetupRepository.Model> GetProcessSetupById(int processId);
    }
}