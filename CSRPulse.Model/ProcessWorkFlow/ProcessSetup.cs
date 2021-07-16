using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class ProcessSetup : BaseModel
    {
        public int SetupId { get; set; }
        public int ProcessId { get; set; }
        public int? RevisionNo { get; set; }
        public int PrimaryRoleId { get; set; }
        public int? SecondoryRoleId { get; set; }
        public int? TertiaryRoleId { get; set; }
        public int? QuaternaryRoleId { get; set; }
        public int LevelId { get; set; }
        public int? Sequence { get; set; }
        public bool Skip { get; set; }       
    }
}
