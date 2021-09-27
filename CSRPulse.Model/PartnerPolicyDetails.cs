using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CSRPulse.Model
{
    public class PartnerPolicyDetails : BaseModel
    {
        public int Id { get; set; }        
        public int PartnerId { get; set; }
        public int PolicyId { get; set; }
        public int? IsApprovedByBoard { get; set; }
        public DateTime? Impletedsince { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

    }
}
