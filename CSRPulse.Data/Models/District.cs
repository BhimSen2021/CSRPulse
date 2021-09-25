using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class District
    {
        public District()
        {
            Block = new HashSet<Block>();
            ProjectLocation = new HashSet<ProjectLocation>();
            ProjectLocationDetail = new HashSet<ProjectLocationDetail>();
            Village = new HashSet<Village>();
        }

        [Key]
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        [Required]
        [StringLength(200)]
        public string DistrictName { get; set; }
        [Required]
        [StringLength(3)]
        public string DistrictCode { get; set; }
        [StringLength(4)]
        public string DistrictShort { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
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

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.DistrictCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.DistrictCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("District")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.DistrictUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.DistrictUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("District")]
        public virtual ICollection<Block> Block { get; set; }
        [InverseProperty("District")]
        public virtual ICollection<ProjectLocation> ProjectLocation { get; set; }
        [InverseProperty("District")]
        public virtual ICollection<ProjectLocationDetail> ProjectLocationDetail { get; set; }
        [InverseProperty("District")]
        public virtual ICollection<Village> Village { get; set; }
    }
}
