using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectInterventionReport
    {
        public int ReportId { get; set; }
        public int ProjectId { get; set; }
        
        public int ReportNo { get; set; }
        [Display(Name ="Due Date")]
        public DateTime DueDate { get; set; }
        [Display(Name = "Report Name")]
        public string ReportName { get; set; }
        [Display(Name = "Project Year")]
        public string ProjectYear { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        
        public int? Status { get; set; }
        [Display(Name = "Submitted By")]
        public int? SubmittedBy { get; set; }
        [Display(Name = "Submitted On")]
        public DateTime? SubmittedOn { get; set; }
        [Display(Name = "Accepted By")]
        public int? AcceptedBy { get; set; }
        [Display(Name = "Accepted On")]
        public DateTime? AcceptedOn { get; set; }
        [Display(Name = "Acceptance Remark")]
        public string AcceptanceRemark { get; set; }
    }
}
