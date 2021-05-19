using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSRPulse.Data.Models;
namespace CSRPulse.Data.Repositories
{
   public interface IPrepareDBForCustomer
    {
        Task<bool> CreateBD(Customer dtoCustomer, string dbPath, string password, out string res);
    }
}
