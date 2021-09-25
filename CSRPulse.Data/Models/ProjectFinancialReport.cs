using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProjectFinancialReport
    {
        [Key]
        public int ReportId { get; set; }
        public int ProjectId { get; set; }
        public int ReportNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DueDate { get; set; }
        [StringLength(20)]
        public string ReportName { get; set; }
        [StringLength(20)]
        public string ProjectYear { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndDate { get; set; }
        public int? Status { get; set; }
        public int? SubmittedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SubmittedOn { get; set; }
        public int? AcceptedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AcceptedOn { get; set; }
        [StringLength(2000)]
        public string AcceptanceRemark { get; set; }
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
        [InverseProperty(nameof(User.ProjectFinancialReportCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.ProjectFinancialReportCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectFinancialReport")]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ProjectFinancialReportUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.ProjectFinancialReportUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
