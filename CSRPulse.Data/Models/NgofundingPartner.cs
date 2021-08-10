using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    [Table("NGOFundingPartner")]
    public partial class NgofundingPartner
    {
        [Key]
        [Column("NGOFundingPartnerId")]
        public int NgofundingPartnerId { get; set; }
        public int PartnerId { get; set; }
        public int AgencyType { get; set; }
        public int FundingAgencyId { get; set; }
        [Required]
        [Column("NGOFundingPartnerName")]
        [StringLength(500)]
        public string NgofundingPartnerName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(FundingAgencyId))]
        [InverseProperty("NgofundingPartner")]
        public virtual FundingAgency FundingAgency { get; set; }
        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("NgofundingPartner")]
        public virtual Partner Partner { get; set; }
    }
}
