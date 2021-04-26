﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerLicenseActivation = new HashSet<CustomerLicenseActivation>();
            CustomerPayment = new HashSet<CustomerPayment>();
        }

        public int CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int? StateID { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string DataBaseName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<CustomerLicenseActivation> CustomerLicenseActivation { get; set; }
        public virtual ICollection<CustomerPayment> CustomerPayment { get; set; }
    }
}