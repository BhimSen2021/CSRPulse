using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CSRPulse.Model
{
    public class EmailConfiguration:BaseModel
    {
        public int EmailConfigurationID { get; set; }
        [StringLength(50, ErrorMessage = "{0} not be exceed 50 char"), DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email.")]
        [Display(Name = "User Name"),Required(ErrorMessage = "Please Enter To Email ID")]
        public string UserName { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Please Enter Password")]
        [StringLength(50)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,30}$", ErrorMessage = "Invalid password patterns.")]
        public string Password { get; set; }
        [Display(Name = "Server")]
        [Required(ErrorMessage ="Server is required")]
        public string Server { get; set; }
        [Display(Name = "Port")]
        [Required(ErrorMessage = "Port is required")]
        public int Port { get; set; }
        [Display(Name = "Ssl status")]
        public bool Sslstatus { get; set; }
        public string FriendlyName { get; set; }

    }
}
