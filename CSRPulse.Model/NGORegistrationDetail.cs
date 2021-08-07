using CSRPulse.Model.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class NGORegistrationDetail : BaseModel
    {        
        [Key]
        public int NgoregistrationDetailId { get; set; }
        public int PartnerId { get; set; }
        [Required, Display(Name = "Constitution")]
        public int ConstitutionId { get; set; }
        [StringLength(100), Required, Display(Name = "Registration Number")]
        public string RegNo { get; set; }

        [CurrentDate(ErrorMessage = "Date of Registration less or equal to current date")]
        [DataType(DataType.Date), Required, Display(Name = "Date of Registration")]
        public DateTime? RegDate { get; set; }

        [StringLength(10), Display(Name = "PAN Number")]
        [RegularExpression(@"^[A-Za-z]{5}\d{4}[A-Za-z]{1}$", ErrorMessage = "Invalid PAN Format")]
        public string Pannumber { get; set; }
        [StringLength(50)]
        [Display(Name = "Certificate of Registration Number u/s 12A")]
        public string RegNoCertificate { get; set; }
        [StringLength(50)]
        [Display(Name = "Approval Number u/s 80G")]
        public string ApprovalNo80G { get; set; }
    }

   
}
