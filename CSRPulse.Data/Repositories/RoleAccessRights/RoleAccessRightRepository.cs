using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSRPulse.Data.Data;
using CSRPulse.Data.Models;

namespace CSRPulse.Data.Repositories
{
    public class RoleAccessRightRepository : BaseRepository, IRoleAccessRightRepository
    {
        public List<Menu> GetMenuList()
        {
            using (_dbContext = new CSRPulseDbContext())
            {
                var lst1 = _dbContext.Menu.Where(x => x.IsActive == true);
                var lst2 = _dbContext.Menu.Where(x => x.IsActive == true);

                var flist = lst1.Where(x => lst2.Any(y => y.ParentMenuId == x.MenuId)).ToList();
                return flist;
            }
        }
    }
}
