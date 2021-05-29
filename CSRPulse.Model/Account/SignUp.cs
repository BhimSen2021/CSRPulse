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
        [StringLength(50, ErrorMessage = "{0} not be exceed 50 char")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email.")]
        public string EmailId { get; set; }

        [Display(Name = "Mobile No"), Required(ErrorMessage = "Please Enter Mobile Number")]
        [StringLength(10, ErrorMessage = "{0} not be exceed 10 char")]
        public string MobileNo { get; set; }

        [Display(Name = "User Name"), Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

        public IFormFileCollection ImageNameFileCollection { get; set; }
        public string ImageName { get; set; }

        [Display(Name = "I accept Terms and Conditions")]
        public bool IsAgree { get; set; }

        public bool RecordExist { get; set; }
    }
}
