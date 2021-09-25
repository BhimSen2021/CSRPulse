using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Village
    {
        public Village()
        {
            ProjectLocationDetail = new HashSet<ProjectLocationDetail>();
        }

        [Key]
        public int VillageId { get; set; }
        public int BlockId { get; set; }
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        [Required]
        [StringLength(50)]
        public string VillageName { get; set; }
        [Required]
        [StringLength(6)]
        public string VillageCode { get; set; }
        public int LocationType { get; set; }
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
        [InverseProperty("Village")]
        public virtual Block Block { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.VillageCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.VillageCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("Village")]
        public virtual District District { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("Village")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.VillageUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.VillageUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("Village")]
        public virtual ICollection<ProjectLocationDetail> ProjectLocationDetail { get; set; }
    }
}
