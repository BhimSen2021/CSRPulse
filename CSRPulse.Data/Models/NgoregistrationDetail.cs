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

        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("NgoregistrationDetail")]
        public virtual Partner Partner { get; set; }
    }
}
