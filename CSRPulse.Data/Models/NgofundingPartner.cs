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
        [Column("CreatedRId")]
        public int CreatedRid { get; set; }
        [Required]
        [Column("CreatedRName")]
        [StringLength(256)]
        public string CreatedRname { get; set; }
        [Column("UpdatedRId")]
        public int? UpdatedRid { get; set; }
        [Column("UpdatedRName")]
        [StringLength(256)]
        public string UpdatedRname { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.NgofundingPartnerCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.NgofundingPartnerCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(FundingAgencyId))]
        [InverseProperty("NgofundingPartner")]
        public virtual FundingAgency FundingAgency { get; set; }
        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("NgofundingPartner")]
        public virtual Partner Partner { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.NgofundingPartnerUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.NgofundingPartnerUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
