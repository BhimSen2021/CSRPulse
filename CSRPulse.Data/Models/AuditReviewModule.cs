using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class AuditReviewModule
    {
        public AuditReviewModule()
        {
            AuditReviewParamter = new HashSet<AuditReviewParamter>();
        }

        [Key]
        public int ModuleId { get; set; }
        [Required]
        [StringLength(256)]
        public string Module { get; set; }
        public int Sequence { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [InverseProperty("Module")]
        public virtual ICollection<AuditReviewParamter> AuditReviewParamter { get; set; }
    }
}
