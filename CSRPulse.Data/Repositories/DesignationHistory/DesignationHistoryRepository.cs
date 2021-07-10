using CSRPulse.Data.Data;
using CSRPulse.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Data.Repositories
{
    public class DesignationHistoryRepository : BaseRepository, IDesignationHistoryRepository
    {

        public async Task<DesignationHistory> GetLastDesignationHistory(int userId)
        {
            var res = await _dbContext.DesignationHistory.OrderByDescending(x => x.DesignationHistoryId).Where(x => x.UserId == userId && x.IsDeleted == false).FirstOrDefaultAsync();
            return res;
        }
    }
}
