using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class PartnerPolicy : BaseModel
    {
        public int PolicyId { get; set; }

        [Required]
        [StringLength(256), MinLength(2, ErrorMessage = "Policy name should be contain atleast 2 characters.")]
        [Display(Name = "Policy Name")]
        public string PolicyName { get; set; }

        public bool RecordExist { get; set; }

        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }

        [Display(Name = "Formally Approved By Board")]
        public bool? IsFormallyApprovedByBoard { get; set; }

        [Display(Name = "Impleted Since")]
        public bool? IsImplementedSince { get; set; }

        [Display(Name = "Last Updated On")]
        public bool? IsLastUpdatedOn { get; set; }

    }
}
