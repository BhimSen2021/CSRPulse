using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Data.Repositories
{
   public interface IPrepareDBForCustomer
    {
        Task<bool> CreateBD(string dbName, string dbPath, string userName, string password, out string res);
    }
}
