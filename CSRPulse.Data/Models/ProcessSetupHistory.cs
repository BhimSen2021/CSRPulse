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
        [Column("ID")]
        public int Id { get; set; }
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
    }
}
