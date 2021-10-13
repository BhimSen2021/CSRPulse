using CSRPulse.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSRPulse.Services
{
    public interface IRoleAccessRightsServices
    {
        Task<List<RoleAccessRights>> GetMenuListAsync();
        Task<List<RoleAccessRights>> GetRoleAccessMenuListAsync(int roleId);
        bool InsertUpdateRoleAccessRights(int roleId, List<RoleAccessRights> listUpdateRoleRights, BaseModel baseModel);
    }
}