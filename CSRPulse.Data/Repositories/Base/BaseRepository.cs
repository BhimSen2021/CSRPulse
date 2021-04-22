using CSRPulse.Data.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace CSRPulse.Data.Repositories
{
    public class BaseRepository : IDisposable, IBaseRepository
    {
        protected internal CSRPulseDbContext _dbContext = null;

        public BaseRepository()
        {
            _dbContext = new CSRPulseDbContext();
        }
        public void Dispose()
        {

        }
    }
}
