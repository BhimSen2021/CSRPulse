using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class NgosaturatoryAuditorDetail : BaseModel
    {
        [Key]
        public int NgosaturatoryAuditorDetailId { get; set; }
        public int PartnerId { get; set; }
        [StringLength(500)]
        [Display(Name = "Name of the Auditing Firm")]
        public string AuditingFirm { get; set; }
        [Display(Name = "Years of association with the organization as an Auditor")]
        public byte? Association { get; set; }
        [Display(Name = "Does the organization have a signed contract with the Auditor?")]
        public bool SignedContract { get; set; }
    }
}
