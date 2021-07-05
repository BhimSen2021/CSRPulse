using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class CustomerPayment : BaseModel
    {
        [Display(Name = "Payment Id")]
        public int PaymentID { get; set; }
        [Display(Name = "Customer Id")]
        public int CustomerID { get; set; }
        [Display(Name = "Amount Paid")]
        public decimal AmountPaid { get; set; }
        [Display(Name = "Payment Mode")]
        public int PaymentMode { get; set; }
        [Display(Name = "Paymane Date")]
        public DateTime PaymentDate { get; set; }
        [Display(Name = "Success")]
        public bool IsSuccess { get; set; }
    }
}
