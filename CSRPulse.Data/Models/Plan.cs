using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Plan
    {
        public Plan()
        {
            CustomerLicenseActivation = new HashSet<CustomerLicenseActivation>();
        }

        [Key]
        public int PlanId { get; set; }
        [Required]
        [StringLength(100)]
        public string PlanName { get; set; }
        [StringLength(500)]
        public string PlanDetail { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [InverseProperty("Plan")]
        public virtual ICollection<CustomerLicenseActivation> CustomerLicenseActivation { get; set; }
    }
}
