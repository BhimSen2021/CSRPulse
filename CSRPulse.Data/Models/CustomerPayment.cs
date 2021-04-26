﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace CSRPulse.Data.Models
{
    public partial class CustomerPayment
    {
        public int PaymentID { get; set; }
        public int CustomerID { get; set; }
        public decimal AmountPaid { get; set; }
        public int PaymentMode { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual Customer Customer { get; set; }
    }
}