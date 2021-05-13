using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CSRPulse.Model
{
    public class Customer : BaseModel
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Please Enter Company Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Address 1")]
        [MinLength(5)]
        [MaxLength(200)]
        public string Address { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Please Select Country")]
        public string Country { get; set; }
        [Display(Name = "State")]
        [Range(1, Int64.MaxValue, ErrorMessage = "Please Select State")]
        [Required(ErrorMessage = "Please Select State")]
        public int? StateId { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Pin Code"), Required(ErrorMessage = "Please Enter Pin Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Phone No.")]
        [Required(ErrorMessage = "Please Enter Phone No.")]
        public string Telephone { get; set; }
        [Display(Name = "Work Email")]
        [Required(ErrorMessage = "Please Enter Work Email")]
        [StringLength(50, ErrorMessage = "{0} not be exceed 50 char")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email.")]
        public string Email { get; set; }
        [Display(Name = "Website")]
        public string Website { get; set; }
        public string DataBaseName { get; set; }

        [Required(ErrorMessage = "Please select term & condition")]
        public bool IsAgree { get; set; }

        public CustomerPayment CustomerPayment { get; set; }
        public CustomerLicenseActivation CustomerLicense { get; set; }
        public IList<CustomerPayment> CustomerPaymentList { get; set; }
        public IList<CustomerLicenseActivation> CustomerLicenseList { get; set; }


        public bool RecordExist { get; set; }

        [Display(Name = "Address 2")]
        [MinLength(5)]
        [MaxLength(200)]
        public string Address2 { get; set; }

        public int? PlainId { get; set; }

        public string OTP { get; set; }

    }
}
