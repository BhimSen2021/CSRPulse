using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    [Table("NGOKeyProjects")]
    public partial class NgokeyProjects
    {
        [Key]
        [Column("NGOKeyProjectId")]
        public int NgokeyProjectId { get; set; }
        public int PartnerId { get; set; }
        [StringLength(100)]
        public string DonorAgency { get; set; }
        [StringLength(500)]
        public string ProjectObjective { get; set; }
        [StringLength(500)]
        public string ProjectLocation { get; set; }
        [StringLength(10)]
        public string ProjectStart { get; set; }
        [StringLength(10)]
        public string ProjectEnd { get; set; }
        [Column(TypeName = "money")]
        public decimal? GrantAmount { get; set; }
        [StringLength(500)]
        public string AdditionalInformation { get; set; }
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

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.NgokeyProjectsCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.NgokeyProjectsCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("NgokeyProjects")]
        public virtual Partner Partner { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.NgokeyProjectsUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.NgokeyProjectsUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
