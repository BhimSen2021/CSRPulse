using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProcessDocument
    {
        public ProcessDocument()
        {
            InverseParentDocument = new HashSet<ProcessDocument>();
            PartnerDocument = new HashSet<PartnerDocument>();
        }

        [Key]
        public int DocumentId { get; set; }
        public int ProcessId { get; set; }
        [Required]
        [StringLength(500)]
        public string DocumentName { get; set; }
        public int? ParentDocumentId { get; set; }
        public int? DocumentMaxSize { get; set; }
        [StringLength(50)]
        public string DocumentType { get; set; }
        public bool? Mandatory { get; set; }
        public bool? DocumentUpload { get; set; }
        [StringLength(500)]
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ProcessDocumentCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(ParentDocumentId))]
        [InverseProperty(nameof(ProcessDocument.InverseParentDocument))]
        public virtual ProcessDocument ParentDocument { get; set; }
        [ForeignKey(nameof(ProcessId))]
        [InverseProperty("ProcessDocument")]
        public virtual Process Process { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ProcessDocumentUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(ProcessDocument.ParentDocument))]
        public virtual ICollection<ProcessDocument> InverseParentDocument { get; set; }
        [InverseProperty("Document")]
        public virtual ICollection<PartnerDocument> PartnerDocument { get; set; }
    }
}
