using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProjectInterventionReport
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

        [ForeignKey(nameof(ProjectId))]
        [InverseProperty("ProjectInterventionReport")]
        public virtual Project Project { get; set; }
    }
}
