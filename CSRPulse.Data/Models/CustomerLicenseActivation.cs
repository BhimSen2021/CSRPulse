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
        public int LicenceActId { get; set; }
        public int CustomerId { get; set; }
        public int PaymentId { get; set; }
        public int PlanId { get; set; }
        [Required]
        public bool? IsTrail { get; set; }
        public int ActivationCount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ActivationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastActivationDate { get; set; }
        public bool IsDeleted { get; set; }
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
        [InverseProperty(nameof(User.CustomerLicenseActivationCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.CustomerLicenseActivationCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("CustomerLicenseActivation")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(PaymentId))]
        [InverseProperty(nameof(CustomerPayment.CustomerLicenseActivation))]
        public virtual CustomerPayment Payment { get; set; }
        [ForeignKey(nameof(PlanId))]
        [InverseProperty("CustomerLicenseActivation")]
        public virtual Plan Plan { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.CustomerLicenseActivationUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.CustomerLicenseActivationUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
