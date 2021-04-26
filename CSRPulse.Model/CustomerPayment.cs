using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
   public class CustomerPayment:BaseModel
    {
        public int PaymentID { get; set; }
        public int CustomerID { get; set; }
        public decimal AmountPaid { get; set; }
        public int PaymentMode { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsSuccess { get; set; }
    }
}
