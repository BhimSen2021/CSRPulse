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
        [Column("PaymentID")]
        public int PaymentId { get; set; }
        [Column("CustomerID")]
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

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("CustomerPayment")]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<CustomerLicenseActivation> CustomerLicenseActivation { get; set; }
    }
}
