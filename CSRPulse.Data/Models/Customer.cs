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
            MailSendStatus = new HashSet<MailSendStatus>();
        }

        [Key]
        public int CustomerId { get; set; }
        [StringLength(10)]
        public string CustomerUniqueId { get; set; }
        [Required]
        [StringLength(30)]
        public string CustomerCode { get; set; }
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(200)]
        public string Address2 { get; set; }
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
        [InverseProperty(nameof(User.CustomerCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.CustomerCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.CustomerUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.CustomerUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<CustomerLicenseActivation> CustomerLicenseActivation { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<CustomerPayment> CustomerPayment { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<MailSendStatus> MailSendStatus { get; set; }
    }
}
