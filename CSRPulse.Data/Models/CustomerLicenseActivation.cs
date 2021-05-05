using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class CustomerLicenseActivation
    {
        [Key]
        [Column("LicenceActID")]
        public int LicenceActId { get; set; }
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [Column("PlanID")]
        public int PlanId { get; set; }
        public int ActivationCount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ActivationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastActivationDate { get; set; }
        public int PaymentId { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("CustomerLicenseActivation")]
        public virtual Customer Customer { get; set; }
        public virtual CustomerPayment Payment { get; set; }
        [ForeignKey(nameof(PlanId))]
        [InverseProperty("CustomerLicenseActivation")]
        public virtual Plan Plan { get; set; }
    }
}
