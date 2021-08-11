using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProcessSetup
    {
        [Key]
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
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? Updatedby { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ProcessSetupCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(PrimaryRoleId))]
        [InverseProperty(nameof(Role.ProcessSetupPrimaryRole))]
        public virtual Role PrimaryRole { get; set; }
        [ForeignKey(nameof(ProcessId))]
        [InverseProperty("ProcessSetup")]
        public virtual Process Process { get; set; }
        [ForeignKey(nameof(QuaternaryRoleId))]
        [InverseProperty(nameof(Role.ProcessSetupQuaternaryRole))]
        public virtual Role QuaternaryRole { get; set; }
        [ForeignKey(nameof(SecondoryRoleId))]
        [InverseProperty(nameof(Role.ProcessSetupSecondoryRole))]
        public virtual Role SecondoryRole { get; set; }
        [ForeignKey(nameof(TertiaryRoleId))]
        [InverseProperty(nameof(Role.ProcessSetupTertiaryRole))]
        public virtual Role TertiaryRole { get; set; }
        [ForeignKey(nameof(Updatedby))]
        [InverseProperty(nameof(User.ProcessSetupUpdatedbyNavigation))]
        public virtual User UpdatedbyNavigation { get; set; }
    }
}
