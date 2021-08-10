using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class NGOFundingPartner : BaseModel
    {
        [Key]
        public int NgofundingPartnerId { get; set; }
        public int PartnerId { get; set; }
        [Display(Name = "Funding Agency Type")]
        public int FundingAgencyId { get; set; }       
        public string FundingAgency { get; set; }
        public int AgencyType { get; set; }
        [Required, StringLength(500), Display(Name = "Funding Agency Name")]
        public string NgofundingPartnerName { get; set; }

    }
}
