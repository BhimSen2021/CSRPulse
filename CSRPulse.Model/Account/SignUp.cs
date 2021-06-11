using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CSRPulse.Model
{
    public class SignUp
    {
        [Display(Name = "Full Name"), Required(ErrorMessage = "Please Enter Full Name")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Display(Name = "Email address"), Required(ErrorMessage = "Please Enter Email address")]
        [StringLength(50, ErrorMessage = "{0} not be exceed 50 char")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email address.")]
        public string EmailId { get; set; }

        [Display(Name = "Mobile No"), Required(ErrorMessage = "Please Enter Mobile Number")]
        [MinLength(10, ErrorMessage = "Please Enter 10 digit mobile number")]
        [StringLength(10, ErrorMessage = "{0} not be exceed 10 char")]
        public string MobileNo { get; set; }

        [Display(Name = "User Name"), Required(ErrorMessage = "Please Enter User Name")]
        [StringLength(50)]
        public string UserName { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Please Enter Password")]
        [StringLength(50)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,30}$", ErrorMessage = "Invalid password patterns.")]
        public string Password { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Please Enter Confirm Password")]
        [Display(Name = "Confirm Password"), Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
      
        public IFormFile ImagePhoto { get; set; }
        public string ImageName { get; set; }

        public bool RecordExist { get; set; }

        public int UserID { get; set; }
    }
}
