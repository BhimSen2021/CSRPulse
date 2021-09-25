using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class PartnerPolicyDetails
    {
        [Key]
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public int PolicyId { get; set; }
        public bool? IsApprovedByBoard { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Impletedsince { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdatedOn { get; set; }
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
