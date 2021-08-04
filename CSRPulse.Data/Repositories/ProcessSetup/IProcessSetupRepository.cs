using System.Linq;

namespace CSRPulse.Data.Repositories
{
    public interface IProcessSetupRepository
    {
        IQueryable<ProcessSetupRepository.Model> GetProcessSetupById(int processId);
        IQueryable<ProcessSetupRepository.Model> GetPSHistoryDetails(int processId, int revisionNo);
        IQueryable<ProcessSetupRepository.SelectListModel> GetRevisionHistoryDropdown(int processId);
    }
}