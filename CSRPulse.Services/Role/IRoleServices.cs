using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Model;
namespace CSRPulse.Services
{
  public interface IRoleServices
    {
        Task<List<Model.Role>> GetRoles();
        Task<bool> CreateRole(Model.Role role);
        Task<bool> UpdateRole(Model.Role role);
        Task<Model.Role> GetRolesById(int roleId);
    }
}
