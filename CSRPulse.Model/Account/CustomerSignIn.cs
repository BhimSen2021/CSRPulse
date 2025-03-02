﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
   public class CustomerSignIn
    {
        [Display(Name = "User Name"), Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        [Display(Name = "Access Key"), Required(ErrorMessage = "Please Enter Access Key")]
        public string CompanyID { get; set; }
        public byte WrongAttemp { get; set; }
    }
}
