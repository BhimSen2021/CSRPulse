using CSRPulse.Data.Models;
using System.Collections.Generic;

namespace CSRPulse.Data.Repositories
{
    public interface IRoleAccessRightRepository
    {
        List<Menu> GetMenuList();   
    }
}