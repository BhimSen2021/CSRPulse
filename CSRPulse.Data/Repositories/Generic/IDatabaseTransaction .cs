using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Data.Repositories.Generic
{
    public interface IDatabaseTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
