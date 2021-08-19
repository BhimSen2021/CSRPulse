using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Auditor : BaseModel
    {
        [Key]
        public int AuditorId { get; set; }
        [Required, Display(Name = "Financial Audit/Impact Assessent Agency")]
        [StringLength(256)]
        public string AuditOrganization { get; set; }
        [StringLength(10)]
        public string Phone { get; set; }
        [StringLength(50, ErrorMessage = "{0} not be exceed 50 char"), DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email.")]
        public string Email { get; set; }
        [StringLength(50)]
        public string Website { get; set; }
        [StringLength(256)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        [Display(Name = "State")]
        public int? StateId { get; set; }
        public string StateName { get; set; }
        [Display(Name = "PIN Code")]
        public int? PinCode { get; set; }
        [StringLength(20), Display(Name = "GST No")]
        public string Gstno { get; set; }

        [StringLength(10), Display(Name = "PAN Number")]
        [RegularExpression(@"^[A-Za-z]{5}\d{4}[A-Za-z]{1}$", ErrorMessage = "Invalid PAN Format")]
        public string Pan { get; set; }
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }
        public List<AuditorDocument> AuditorDocument { get; set; }
    }


}
