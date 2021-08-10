using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class ProcessSetupHistory : BaseModel
    {
        public int PshistoryId { get; set; }
        public int? RevisionNo { get; set; }
        public int ProcessId { get; set; }
        public int? PrimaryRoleId { get; set; }
        public string PrimaryRole { get; set; }
        public int? SecondoryRoleId { get; set; }
        public string SecondoryRole { get; set; }
        public int? TertiaryRoleId { get; set; }
        public string TertiaryRole { get; set; }
        public int? QuaternaryRoleId { get; set; }
        public string QuaternaryRole { get; set; }
        public int LevelId { get; set; }
        public int Sequence { get; set; }
        public bool? Skip { get; set; }
    }
}
