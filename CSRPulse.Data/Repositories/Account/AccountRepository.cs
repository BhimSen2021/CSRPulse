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
    }
}
