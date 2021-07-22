using CSRPulse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Data.Repositories
{
    public class ProcessSetupRepository : BaseRepository, IProcessSetupRepository
    {
        public IQueryable<Model> GetProcessSetupById(int processId)
        {
            var result = from e1 in _dbContext.ProcessSetup
                         join e2 in _dbContext.Role on e1.PrimaryRoleId equals e2.RoleId into er1
                         from x1 in er1.DefaultIfEmpty()
                         join e3 in _dbContext.Role on e1.SecondoryRoleId equals e3.RoleId into er2
                         from x2 in er2.DefaultIfEmpty()
                         join e4 in _dbContext.Role on e1.TertiaryRoleId equals e4.RoleId into er3
                         from x3 in er3.DefaultIfEmpty()
                         join e5 in _dbContext.Role on e1.QuaternaryRoleId equals e5.RoleId into er4
                         from x4 in er4.DefaultIfEmpty()

                         select new Model
                         {
                             SetupId = e1.SetupId,
                             ProcessId = e1.ProcessId,
                             RevisionNo = e1.RevisionNo,
                             PrimaryRoleId = e1.PrimaryRoleId,
                             PrimaryRole = x1.RoleName,
                             SecondoryRoleId = e1.SecondoryRoleId,
                             SecondoryRole = x2.RoleName,
                             TertiaryRoleId = e1.TertiaryRoleId,
                             TertiaryRole = x3.RoleName,
                             QuaternaryRoleId = e1.QuaternaryRoleId,
                             QuaternaryRole = x4.RoleName,
                             LevelId = e1.LevelId,
                             Skip = e1.Skip,
                             Sequence = e1.Sequence

                         };



            return result;
        }

        public class Model
        {
            public int SetupId { get; set; }
            public int ProcessId { get; set; }
            public int? RevisionNo { get; set; }
            public int? PrimaryRoleId { get; set; }
            public string PrimaryRole { get; set; }
            public int? SecondoryRoleId { get; set; }
            public string SecondoryRole { get; set; }
            public int? TertiaryRoleId { get; set; }
            public string TertiaryRole { get; set; }
            public int? QuaternaryRoleId { get; set; }
            public string QuaternaryRole { get; set; }
            public int LevelId { get; set; }
            public bool Skip { get; set; }
            public int? Sequence { get; set; }
        }
    }
}
