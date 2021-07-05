using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IDepartmentServices
    {
        bool ActiveDeActive(int id, bool IsActive);
        Task<bool> CreateDepartment(Department department);
        Task<Department> GetDepartmentById(int DepartmentId);
        Task<List<Department>> GetDepartmentList();
        Task<List<Department>> GetDepartmentsAsync(Department department);
        Task<bool> RecordExist(Department department);
        Task<bool> UpdateDepartment(Department department);
    }
}