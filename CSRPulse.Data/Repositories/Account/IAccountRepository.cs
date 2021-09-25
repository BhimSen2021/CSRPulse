using System.Linq;

namespace CSRPulse.Data.Repositories
{
    public interface IAccountRepository
    {
        bool VerifyUser(string sUserName, out string dbPassword);
        IQueryable<UserRole> GetUserforAssignRole(int userId);
    }
}