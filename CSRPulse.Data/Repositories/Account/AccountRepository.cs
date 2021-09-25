using CSRPulse.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSRPulse.Data.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public bool VerifyUser(string sUserName, out string dbPassword)
        {
            bool isExists = false;
            dbPassword = "";
            using (_dbContext = new CSRPulseDbContext())
            {
                var user = _dbContext.User.Where(x => x.UserName.Equals(sUserName) && x.IsActive == true).FirstOrDefault();
                if (user != null)
                {
                    dbPassword = user.Password;
                    isExists = true;
                }
            }
            return isExists;
        }

        public IQueryable<UserRole> GetUserforAssignRole(int userId)
        {
            return (from r in _dbContext.Role.Where(x => x.RoleId != 99 && x.RoleId!=51 && x.RoleId != 52 && x.RoleId != 70 && x.RoleId != 71)
                    join a in _dbContext.UserRole.Where(u => u.UserId == userId)
                    on r.RoleId equals a.RoleId into assigned
                    from au in assigned.DefaultIfEmpty()
                    select new UserRole
                    {
                        RoleId = r.RoleId,
                        RoleName = r.RoleName,
                        AssigneRole = au.RoleId > 0 ? true : false
                    });
        }
    }

    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool AssigneRole { get; set; }
    }
}
