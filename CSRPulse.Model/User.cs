using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class User : BaseModel
    {
        public int UserID { get; set; }
        public int UserTypeID { get; set; }

        [Display(Name = "User ID"), Required(ErrorMessage = "Please Enter User ID")]
        public string UserName { get; set; }

        [Display(Name = "User Name"), Required(ErrorMessage = "Please Enter User Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile Number")]
        public string MobileNo { get; set; }
        [Display(Name = "Work Email"), Required(ErrorMessage = "Please Enter Work Email ID")]
        [StringLength(50, ErrorMessage = "{0} not be exceed 50 char"), DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email.")]
        public string EmailID { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Please Enter Password")]
        [StringLength(50)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,30}$", ErrorMessage = "Invalid password patterns.")]
        public string Password { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Please Enter Confirm Password")]
        [Display(Name = "Confirm Password"), Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string ImageName { get; set; }
        public int? DefaultMenuId { get; set; }
       
        public byte? WrongAttemp { get; set; }      
        public DateTime? LockDate { get; set; }      
        public DateTime? LastLogin { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }

        public IFormFile ImagePhoto { get; set; }     

        public bool RecordExist { get; set; }
    }
}
