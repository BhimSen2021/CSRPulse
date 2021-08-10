using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Partner
    {
        public Partner()
        {
            NgoawardDetail = new HashSet<NgoawardDetail>();
            NgochartDocument = new HashSet<NgochartDocument>();
            NgocorpusGrantFund = new HashSet<NgocorpusGrantFund>();
            NgofundingPartner = new HashSet<NgofundingPartner>();
            NgokeyProjects = new HashSet<NgokeyProjects>();
            Ngomember = new HashSet<Ngomember>();
            NgoregistrationDetail = new HashSet<NgoregistrationDetail>();
            NgosaturatoryAuditorDetail = new HashSet<NgosaturatoryAuditorDetail>();
        }

        [Key]
        public int PartnerId { get; set; }
        [Required]
        [StringLength(256)]
        public string PartnerName { get; set; }
        public int PartnerType { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Website { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateOfEst { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DealingWithUsSince { get; set; }
        public string PartnerDetails { get; set; }
        public string MissionVision { get; set; }
        [Required]
        [StringLength(256)]
        public string RegAddress { get; set; }
        [Required]
        [StringLength(50)]
        public string RegCity { get; set; }
        [Column("RegPIN")]
        public int RegPin { get; set; }
        public int RegState { get; set; }
        [StringLength(12)]
        public string RegPhone { get; set; }
        [StringLength(10)]
        public string RegMobile { get; set; }
        [StringLength(256)]
        public string CommAddress { get; set; }
        [StringLength(50)]
        public string ComCity { get; set; }
        [Column("ComPIN")]
        public int? ComPin { get; set; }
        public int? ComState { get; set; }
        [StringLength(12)]
        public string CommPhone { get; set; }
        [StringLength(10)]
        public string CommMobile { get; set; }
        public int? ProgramType { get; set; }
        public bool? IsReligiousPoliticalObjectives { get; set; }
        [StringLength(500)]
        public string ReligiousPoliticalObjectives { get; set; }
        public bool? IsConstitutionalDocument { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [InverseProperty("Partner")]
        public virtual ICollection<NgoawardDetail> NgoawardDetail { get; set; }
        [InverseProperty("Partner")]
        public virtual ICollection<NgochartDocument> NgochartDocument { get; set; }
        [InverseProperty("Partner")]
        public virtual ICollection<NgocorpusGrantFund> NgocorpusGrantFund { get; set; }
        [InverseProperty("Partner")]
        public virtual ICollection<NgofundingPartner> NgofundingPartner { get; set; }
        [InverseProperty("Partner")]
        public virtual ICollection<NgokeyProjects> NgokeyProjects { get; set; }
        [InverseProperty("Partner")]
        public virtual ICollection<Ngomember> Ngomember { get; set; }
        [InverseProperty("Partner")]
        public virtual ICollection<NgoregistrationDetail> NgoregistrationDetail { get; set; }
        [InverseProperty("Partner")]
        public virtual ICollection<NgosaturatoryAuditorDetail> NgosaturatoryAuditorDetail { get; set; }
    }
}
