using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class AuditorDocument
    {
        [Key]
        public int AuditorDocumentId { get; set; }
        public int AuditorId { get; set; }
        public int DocumentId { get; set; }
        [Required]
        [Column("UDName")]
        [StringLength(50)]
        public string Udname { get; set; }
        [Required]
        [Column("SDName")]
        [StringLength(50)]
        public string Sdname { get; set; }
        [StringLength(256)]
        public string Remark { get; set; }
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

        [ForeignKey(nameof(AuditorId))]
        [InverseProperty("AuditorDocument")]
        public virtual Auditor Auditor { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.AuditorDocumentCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.AuditorDocumentCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(DocumentId))]
        [InverseProperty(nameof(ProcessDocument.AuditorDocument))]
        public virtual ProcessDocument Document { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.AuditorDocumentUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.AuditorDocumentUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
