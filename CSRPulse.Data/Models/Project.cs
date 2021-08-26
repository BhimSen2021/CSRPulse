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
            ProjectFinancialReport = new HashSet<ProjectFinancialReport>();
            ProjectInternalSource = new HashSet<ProjectInternalSource>();
            ProjectInterventionReport = new HashSet<ProjectInterventionReport>();
            ProjectLocation = new HashSet<ProjectLocation>();
            ProjectLocationDetail = new HashSet<ProjectLocationDetail>();
            ProjectOtherSource = new HashSet<ProjectOtherSource>();
            ProjectReport = new HashSet<ProjectReport>();
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

        [ForeignKey(nameof(PartnerId))]
        [InverseProperty("Project")]
        public virtual Partner Partner { get; set; }
        [ForeignKey(nameof(ProgramManagerId))]
        [InverseProperty(nameof(User.Project))]
        public virtual User ProgramManager { get; set; }
        [ForeignKey(nameof(SubThemeId))]
        [InverseProperty("Project")]
        public virtual SubTheme SubTheme { get; set; }
        [ForeignKey(nameof(ThemeId))]
        [InverseProperty("Project")]
        public virtual Theme Theme { get; set; }
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
        public virtual ICollection<ProjectOtherSource> ProjectOtherSource { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ProjectReport> ProjectReport { get; set; }
    }
}
