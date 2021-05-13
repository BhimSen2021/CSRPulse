using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace CSRPulse.Model
{
    public class CustomerLicenseActivation : BaseModel
    {
        [Display(Name = "License Activation Id")]
        public int LicenceActID { get; set; }
        [Display(Name = "Customer Id")]
        public int CustomerID { get; set; }
        [Display(Name = "Plan Id")]
        public int PlanID { get; set; }
        [Display(Name = "Activation Count")]
        public int ActivationCount { get; set; }
        [Display(Name = "Activation Date")]
        public DateTime ActivationDate { get; set; }
        [Display(Name = "Activation Last Date")]
        public DateTime LastActivationDate { get; set; }
        public int PaymentId { get; set; }
    }
}
