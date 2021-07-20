using CSRPulse.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class ProcessSetup : BaseModel
    {
        public int SetupId { get; set; }
        public int ProcessId { get; set; }
        [Display(Name = "Revision No")]
        public int RevisionNo { get; set; }
        public int PrimaryRoleId { get; set; }
        [Display(Name = "Primary Role")]
        public string PrimaryRole { get; set; }
        public int? SecondoryRoleId { get; set; }
        [Display(Name = "Secondory Role")]
        public string SecondoryRole { get; set; }
        public int? TertiaryRoleId { get; set; }
        [Display(Name = "Tertiary Role")]
        public string TertiaryRole { get; set; }
        public int? QuaternaryRoleId { get; set; }
        [Display(Name = "Quaternary Role")]
        public string QuaternaryRole { get; set; }
        [Display(Name = "Level Name")]
        public int LevelId { get; set; }
        public ProcessLevel level { get; set; }
        public int Sequence { get; set; }
        public bool Skip { get; set; }
    }
}
