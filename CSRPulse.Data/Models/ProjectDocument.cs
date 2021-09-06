using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProjectDocument
    {
        [Key]
        public int ProjectDocumentId { get; set; }
        public int ProjectId { get; set; }
        public int DocumentId { get; set; }
        [StringLength(256)]
        public string DocumentName { get; set; }
        [StringLength(256)]
        public string ServerDocumentName { get; set; }

        [ForeignKey(nameof(DocumentId))]
        [InverseProperty(nameof(ProcessDocument.ProjectDocument))]
        public virtual ProcessDocument Document { get; set; }
        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectDocument")]
        public virtual Project Project { get; set; }
    }
}
