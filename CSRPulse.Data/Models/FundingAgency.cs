using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class FundingAgency
    {
        public FundingAgency()
        {
            NgofundingPartner = new HashSet<NgofundingPartner>();
        }

        [Key]
        public int FundingAgencyId { get; set; }
        [Required]
        [StringLength(250)]
        public string AgencyName { get; set; }
        public byte AgencyType { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [InverseProperty("FundingAgency")]
        public virtual ICollection<NgofundingPartner> NgofundingPartner { get; set; }
    }
}
