using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class FundingAgency : BaseModel
    {
        public int FundingAgencyId { get; set; }

        [StringLength(250)]
        public string AgencyName { get; set; }
        public byte AgencyType { get; set; }
    }
}
