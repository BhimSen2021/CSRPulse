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

        [ForeignKey(nameof(DocumentId))]
        [InverseProperty(nameof(ProcessDocument.PartnerDocument))]
        public virtual ProcessDocument Document { get; set; }
        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("PartnerDocument")]
        public virtual Partner Partner { get; set; }
    }
}
