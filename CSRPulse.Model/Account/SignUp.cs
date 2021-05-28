using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CSRPulse.Model
{
   public class SignUp
    {
        [Display(Name = "Full Name"), Required(ErrorMessage = "Please Enter Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Email address"), Required(ErrorMessage = "Please Enter Email address")]
        public string EmailId { get; set; }

        [Display(Name = "User Name"), Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        [Display(Name = "I accept Terms and Conditions")]
        public bool TermConditions { get; set; }
    }
}
