using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProjectLocationDetail
    {
        [Key]
        public int LocationId { get; set; }
        public int ProjectId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int? BlockId { get; set; }
        public int? VillageId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
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

        [ForeignKey(nameof(BlockId))]
        [InverseProperty("ProjectLocationDetail")]
        public virtual Block Block { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ProjectLocationDetailCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.ProjectLocationDetailCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("ProjectLocationDetail")]
        public virtual District District { get; set; }
        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectLocationDetail")]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("ProjectLocationDetail")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ProjectLocationDetailUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.ProjectLocationDetailUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [ForeignKey(nameof(VillageId))]
        [InverseProperty("ProjectLocationDetail")]
        public virtual Village Village { get; set; }
    }
}
