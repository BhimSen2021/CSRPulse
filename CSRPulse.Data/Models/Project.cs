using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectCommunication = new HashSet<ProjectCommunication>();
            ProjectDocument = new HashSet<ProjectDocument>();
            ProjectFinancialReport = new HashSet<ProjectFinancialReport>();
            ProjectInternalSource = new HashSet<ProjectInternalSource>();
            ProjectInterventionReport = new HashSet<ProjectInterventionReport>();
            ProjectLocation = new HashSet<ProjectLocation>();
            ProjectLocationDetail = new HashSet<ProjectLocationDetail>();
            ProjectNarrativeAnswer = new HashSet<ProjectNarrativeAnswer>();
            ProjectNarrativeQuestion = new HashSet<ProjectNarrativeQuestion>();
            ProjectOtherSource = new HashSet<ProjectOtherSource>();
            ProjectReport = new HashSet<ProjectReport>();
            ProjectTeamDetail = new HashSet<ProjectTeamDetail>();
        }

        [Key]
        public int ProjectId { get; set; }
        [Required]
        [StringLength(4000)]
        public string ProjectName { get; set; }
        [StringLength(2000)]
        public string ShortName { get; set; }
        public int ProgramManagerId { get; set; }
        public int PartnerId { get; set; }
        public int ThemeId { get; set; }
        public int SubThemeId { get; set; }
        public int LocationLavel { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalBudget { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TrustContribution { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? OtherSource { get; set; }
        public int Status { get; set; }
        public int ReportType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExtentDate { get; set; }
        public int? ExtendedBy { get; set; }
        [StringLength(2000)]
        public string ExtendComments { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
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
        [InverseProperty(nameof(User.ProjectCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.ProjectCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("Project")]
        public virtual Partner Partner { get; set; }
        [ForeignKey(nameof(ProgramManagerId))]
        [InverseProperty(nameof(User.ProjectProgramManager))]
        public virtual User ProgramManager { get; set; }
        [ForeignKey(nameof(SubThemeId))]
        [InverseProperty("Project")]
        public virtual SubTheme SubTheme { get; set; }
        [ForeignKey(nameof(ThemeId))]
        [InverseProperty("Project")]
        public virtual Theme Theme { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ProjectUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.ProjectUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectCommunication> ProjectCommunication { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectDocument> ProjectDocument { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectFinancialReport> ProjectFinancialReport { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectInternalSource> ProjectInternalSource { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectInterventionReport> ProjectInterventionReport { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectLocation> ProjectLocation { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectLocationDetail> ProjectLocationDetail { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectNarrativeAnswer> ProjectNarrativeAnswer { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectNarrativeQuestion> ProjectNarrativeQuestion { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectOtherSource> ProjectOtherSource { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectReport> ProjectReport { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectTeamDetail> ProjectTeamDetail { get; set; }
    }
}
