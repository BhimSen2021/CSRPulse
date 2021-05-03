using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class CustomerLicenseActivation
    {
        public int LicenceActId { get; set; }
        public int CustomerId { get; set; }
        public int PlanId { get; set; }
        public int ActivationCount { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime LastActivationDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Plan Plan { get; set; }
    }
}
