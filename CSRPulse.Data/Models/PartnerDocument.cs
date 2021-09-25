using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class PartnerDocument
    {
        [Key]
        public int PartnerDocumentId { get; set; }
        public int PartnerId { get; set; }
        public int DocumentId { get; set; }
        [Required]
        [StringLength(256)]
        public string UploadedDocumentName { get; set; }
        [Required]
        [StringLength(256)]
        public string ServerDocumentName { get; set; }
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

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.PartnerDocumentCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.PartnerDocumentCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(DocumentId))]
        [InverseProperty(nameof(ProcessDocument.PartnerDocument))]
        public virtual ProcessDocument Document { get; set; }
        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("PartnerDocument")]
        public virtual Partner Partner { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.PartnerDocumentUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.PartnerDocumentUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
