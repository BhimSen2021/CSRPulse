using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class PartnerPolicyModule
    {
        [Key]
        public int PolicyModuleId { get; set; }
        public int PolicyId { get; set; }
        [Required]
        [StringLength(256)]
        public string PolicyModuleName { get; set; }
        public bool? IsFormallyApprovedByBoard { get; set; }
        public bool? IsImplementedSince { get; set; }
        public bool? IsLastUpdatedOn { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
