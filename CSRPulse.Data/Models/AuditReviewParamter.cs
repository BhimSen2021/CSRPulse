using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class AuditReviewParamter
    {
        [Key]
        public int ParamterId { get; set; }
        public int ModuleId { get; set; }
        [Required]
        public string Scope { get; set; }
        public string ReviewInstruction { get; set; }
        public byte Critical { get; set; }
        public int? Sequence { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? ReferenceNo { get; set; }
        public byte MaximumMarks { get; set; }
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

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.AuditReviewParamterCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.AuditReviewParamterCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(ModuleId))]
        [InverseProperty(nameof(AuditReviewModule.AuditReviewParamter))]
        public virtual AuditReviewModule Module { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.AuditReviewParamterUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.AuditReviewParamterUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
