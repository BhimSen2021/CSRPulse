using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    [Table("NGORegistrationDetail")]
    public partial class NgoregistrationDetail
    {
        [Key]
        [Column("NGORegistrationDetailId")]
        public int NgoregistrationDetailId { get; set; }
        public int PartnerId { get; set; }
        public int? ConstitutionId { get; set; }
        [Required]
        [StringLength(100)]
        public string RegNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RegDate { get; set; }
        [Column("PANNumber")]
        [StringLength(10)]
        public string Pannumber { get; set; }
        [StringLength(50)]
        public string RegNoCertificate { get; set; }
        [StringLength(50)]
        public string ApprovalNo80G { get; set; }
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
        [InverseProperty(nameof(User.NgoregistrationDetailCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.NgoregistrationDetailCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("NgoregistrationDetail")]
        public virtual Partner Partner { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.NgoregistrationDetailUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.NgoregistrationDetailUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
