using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model.ProcessWorkFlow
{
    public class ProcessSetup
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
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? Updatedby { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
