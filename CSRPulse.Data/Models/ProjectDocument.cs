﻿using System;
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
        [InverseProperty(nameof(User.ProjectDocumentCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.ProjectDocumentCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(DocumentId))]
        [InverseProperty(nameof(ProcessDocument.ProjectDocument))]
        public virtual ProcessDocument Document { get; set; }
        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectDocument")]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ProjectDocumentUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.ProjectDocumentUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
