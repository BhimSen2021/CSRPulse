using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CSRPulse.Model
{
    public class ChangePassword
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Old Password"), DataType(DataType.Password), Required(ErrorMessage = "Please Enter Old Password"), StringLength(50)]
        public string OldPassword { get; set; }
        public string hdOldPassword { get; set; }

        [Display(Name = "New Password"), StringLength(50), DataType(DataType.Password), Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        public string hdPassword { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Please Enter Confirm Password")]
        [Display(Name = "Confirm Password"), Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string hdConfirmPassword { get; set; }
    }
}
