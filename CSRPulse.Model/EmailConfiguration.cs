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
        [Display(Name = "To Email"), Required(ErrorMessage = "Please Enter To Email ID")]
        [StringLength(50, ErrorMessage = "{0} not be exceed 50 char"), DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email.")]
        public string ToEmail { get; set; }
        [Display(Name = "Bcc")]
        public string Bcc { get; set; }
        [Display(Name = "Cc Email")]
        public string CcEmail { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Please Enter Password")]
        [StringLength(50)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,30}$", ErrorMessage = "Invalid password patterns.")]
        //[Display(Name = "Password")]
        //[Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Display(Name = "Server")]
        [Required(ErrorMessage ="Server is required")]
        public string Server { get; set; }
        [Display(Name = "Port")]
        [Required(ErrorMessage = "Port is required")]
        public int Port { get; set; }
        [Display(Name = "Signature")]
        public string Signature { get; set; }
        [Display(Name = "Ssl status")]
        public bool Sslstatus { get; set; }
        //public int CreatedBy { get; set; }
        //[Column(TypeName = "datetime")]
        //public DateTime CreatedOn { get; set; }
        //public int? UpdatedBy { get; set; }
        //[Column(TypeName = "datetime")]
        //public DateTime? UpdatedOn { get; set; }
        //public bool IsDeleted { get; set; }


    }
}
