using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IDesignationServices
    {
        bool ActiveDeActive(int id, bool IsActive);
        Task<bool> CreateDesignation(Designation designation);
        Task<Designation> GetDesignationById(int DesignationId);
        Task<List<Designation>> GetDesignationsAsync(Designation designation);
        Task<bool> RecordExist(Designation designation);
        Task<bool> UpdateDesignation(Designation designation);
    }
}