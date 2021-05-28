using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class ForgotPassword
    {
        [Display(Name = "User Name"), Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }

        [Display(Name = "Access Key")]
        public string CompanyID { get; set; }
        [Required(ErrorMessage ="Please Enter OTP.")]
        public string OTP { get; set; }

    }
}
