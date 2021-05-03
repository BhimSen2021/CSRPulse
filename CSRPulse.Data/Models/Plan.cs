using System;
using System.Collections.Generic;

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

        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string PlanDetail { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<CustomerLicenseActivation> CustomerLicenseActivation { get; set; }
    }
}
