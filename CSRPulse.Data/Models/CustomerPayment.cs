using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class CustomerPayment
    {
        public CustomerPayment()
        {
            CustomerLicenseActivation = new HashSet<CustomerLicenseActivation>();
        }

        [Key]
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        [Column(TypeName = "money")]
        public decimal AmountPaid { get; set; }
        public int PaymentMode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PaymentDate { get; set; }
        public bool IsSuccess { get; set; }
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
        [InverseProperty(nameof(User.CustomerPaymentCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.CustomerPaymentCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("CustomerPayment")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.CustomerPaymentUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.CustomerPaymentUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("Payment")]
        public virtual ICollection<CustomerLicenseActivation> CustomerLicenseActivation { get; set; }
    }
}
