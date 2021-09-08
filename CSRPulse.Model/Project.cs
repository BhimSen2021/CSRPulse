using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Project : BaseModel
    {

        public int ProjectId { get; set; }
        [Required, StringLength(4000), Display(Name = "Name of the Project")]
        public string ProjectName { get; set; }
        [StringLength(2000), Display(Name = "Project Short Name")]
        public string ShortName { get; set; }
        [Required, Display(Name = "Name of the Organisation")]
        public int PartnerId { get; set; }
        [Required, Display(Name = "Program Manager")]
        public int ProgramManagerId { get; set; }

        [Required, Display(Name = "Theme")]
        public int ThemeId { get; set; }
        [Required, Display(Name = "SubTheme")]
        public int SubThemeId { get; set; }
        [Required, Display(Name = "Implementation Level")]
        public int LocationLavel { get; set; }
        [Required, Display(Name = "Implementation Location")]
        public string hdnLocationIds { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Project Start Date")]
        public DateTime? StartDate { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Project End Date")]
        public DateTime? EndDate { get; set; }

        [Required, Display(Name = "Total Budget (₹)")]
        public decimal TotalBudget { get; set; }
        [Required, Display(Name = "Total Project Support from Internal(₹)")]
        public decimal TrustContribution { get; set; }

        [Display(Name = "Other Source Contribution (₹)")]
        public decimal? OtherSource { get; set; }
        [Display(Name = "Project Status")]
        public int Status { get; set; }
        [Required, Display(Name = "Report Type")]
        public int ReportType { get; set; }
        public DateTime? ExtentDate { get; set; }
        public int? ExtendedBy { get; set; }
        [StringLength(2000)]
        public string ExtendComments { get; set; }
        public List<ProjectOtherSource> ProjectOtherSource { get; set; }
        public List<ProjectInternalSource> ProjectInternalSource { get; set; }
        public List<ProjectInterventionReport> ProjectInterventionReport { get; set; }
        public List<ProjectFinancialReport> ProjectFinancialReport { get; set; }
        public List<ProjectReport> ProjectReport { get; set; }
        public List<ProjectLocation> ProjectLocation { get; set; }
        public List<ProjectLocationDetail> ProjectLocationDetail { get; set; }
        public List<ProjectDocument> ProjectDocument { get; set; }
        public List<ProjectCommunication> ProjectCommunication { get; set; }
        public ProjectCommunication Communication { get; set; }
    }
}
