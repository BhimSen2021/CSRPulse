using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class ForgotPassword
    {
        [Display(Name = "Email Id"), Required(ErrorMessage = "Please Enter Email Id"), StringLength(50, ErrorMessage = "{0} not be exceed 50 char"), DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email.")]
        public string EmailId { get; set; }

        [Display(Name = "Access Key")]
        public string CompanyID { get; set; }
        [Required(ErrorMessage = "Please Enter OTP.")]
        public string OTP { get; set; }
        public string Password { get; set; }

    }
}
