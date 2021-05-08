using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerLicenseActivation = new HashSet<CustomerLicenseActivation>();
            CustomerPayment = new HashSet<CustomerPayment>();
        }

        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(30)]
        public string CustomerCode { get; set; }
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        public int? StateId { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(30)]
        public string PostalCode { get; set; }
        [StringLength(50)]
        public string Telephone { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Website { get; set; }
        [StringLength(200)]
        public string DataBaseName { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column("address2")]
        [StringLength(200)]
        public string Address2 { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<CustomerLicenseActivation> CustomerLicenseActivation { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<CustomerPayment> CustomerPayment { get; set; }
    }
}
