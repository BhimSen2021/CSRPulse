using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    [Table("NGOSaturatoryAuditorDetail")]
    public partial class NgosaturatoryAuditorDetail
    {
        [Key]
        [Column("NGOSaturatoryAuditorDetailId")]
        public int NgosaturatoryAuditorDetailId { get; set; }
        public int PartnerId { get; set; }
        [StringLength(500)]
        public string AuditingFirm { get; set; }
        public byte? Association { get; set; }
        public bool? SignedContract { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("NgosaturatoryAuditorDetail")]
        public virtual Partner Partner { get; set; }
    }
}
