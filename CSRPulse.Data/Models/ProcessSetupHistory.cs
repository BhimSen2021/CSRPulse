using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProcessSetupHistory
    {
        [Key]
        [Column("PSHistoryId")]
        public int PshistoryId { get; set; }
        public int? RevisionNo { get; set; }
        public int ProcessId { get; set; }
        public int? PrimaryRoleId { get; set; }
        [Column("SecondoryRoleID")]
        public int? SecondoryRoleId { get; set; }
        public int? TertiaryRoleId { get; set; }
        public int? QuaternaryRoleId { get; set; }
        public int LevelId { get; set; }
        public int Sequence { get; set; }
        public bool? Skip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? Updatedby { get; set; }
        [StringLength(2)]
        public string Flag { get; set; }
        [Column("CreatedRId")]
        public int CreatedRid { get; set; }
        [Required]
        [Column("CreatedRName")]
        [StringLength(256)]
        public string CreatedRname { get; set; }
        [Column("UpdatedRId")]
        public int? UpdatedRid { get; set; }
        [Column("UpdatedRName")]
        [StringLength(256)]
        public string UpdatedRname { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ProcessSetupHistoryCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.ProcessSetupHistoryCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.ProcessSetupHistoryUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [ForeignKey(nameof(Updatedby))]
        [InverseProperty(nameof(User.ProcessSetupHistoryUpdatedbyNavigation))]
        public virtual User UpdatedbyNavigation { get; set; }
    }
}
