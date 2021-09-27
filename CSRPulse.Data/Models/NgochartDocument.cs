using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    [Table("NGOChartDocument")]
    public partial class NgochartDocument
    {
        [Key]
        [Column("NGOChartDocumentId")]
        public int NgochartDocumentId { get; set; }
        public int PartnerId { get; set; }
        [Required]
        [StringLength(250)]
        public string DocumentName { get; set; }
        [Required]
        [StringLength(256)]
        public string ServerDocumentName { get; set; }
        [StringLength(2000)]
        public string Remarks { get; set; }
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
        [InverseProperty(nameof(User.NgochartDocumentCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.NgochartDocumentCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("NgochartDocument")]
        public virtual Partner Partner { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.NgochartDocumentUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.NgochartDocumentUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
