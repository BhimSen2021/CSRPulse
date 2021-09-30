using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProjectNarrativeQuestion
    {
        public ProjectNarrativeQuestion()
        {
            ProjectNarrativeAnswer = new HashSet<ProjectNarrativeAnswer>();
        }

        [Key]
        public int ProjectQuestionId { get; set; }
        public int ProjectId { get; set; }
        public int QuestionId { get; set; }
        [Required]
        [StringLength(500)]
        public string Question { get; set; }
        public int ProcessId { get; set; }
        public bool? CommentRequire { get; set; }
        [Required]
        [StringLength(8)]
        public string QuestionType { get; set; }
        public int? ContentLimit { get; set; }
        public int? OrderNo { get; set; }
        public bool IsActive { get; set; }
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
        [InverseProperty(nameof(User.ProjectNarrativeQuestionCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.ProjectNarrativeQuestionCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectNarrativeQuestion")]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(QuestionId))]
        [InverseProperty(nameof(NarrativeQuestion.ProjectNarrativeQuestion))]
        public virtual NarrativeQuestion QuestionNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ProjectNarrativeQuestionUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.ProjectNarrativeQuestionUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("ProjectQuestion")]
        public virtual ICollection<ProjectNarrativeAnswer> ProjectNarrativeAnswer { get; set; }
    }
}
