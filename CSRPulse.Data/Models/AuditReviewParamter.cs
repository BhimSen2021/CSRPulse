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

        [ForeignKey(nameof(ModuleId))]
        [InverseProperty(nameof(AuditReviewModule.AuditReviewParamter))]
        public virtual AuditReviewModule Module { get; set; }
    }
}
