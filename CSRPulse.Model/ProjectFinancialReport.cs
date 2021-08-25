using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectFinancialReport
    {
        public int ReportId { get; set; }
        public int ProjectId { get; set; }
        public int ReportNo { get; set; }       
        public DateTime DueDate { get; set; }       
        public string ReportName { get; set; }       
        public string ProjectYear { get; set; }       
        public DateTime StartDate { get; set; }       
        public DateTime EndDate { get; set; }
        public int? Status { get; set; }
        public int? SubmittedBy { get; set; }       
        public DateTime? SubmittedOn { get; set; }
        public int? AcceptedBy { get; set; }        
        public DateTime? AcceptedOn { get; set; }       
        public string AcceptanceRemark { get; set; }

    }
}
