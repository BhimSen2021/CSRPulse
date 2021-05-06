using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
   public class CustomerLicenseActivation:BaseModel
    {
        public int LicenceActID { get; set; }
        public int CustomerID { get; set; }
        public int PlanID { get; set; }
        public int ActivationCount { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime LastActivationDate { get; set; }
        public int PaymentId { get; set; }
    }
}
