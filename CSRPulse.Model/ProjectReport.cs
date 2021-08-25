using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
   public class ProjectReport
    {
        [Key]
        public int ProjectReportId { get; set; }
        [Display(Name = "Report Number")]
        public int ReportNo { get; set; }
        [StringLength(10),Display(Name = "Report Name")]
        public string ReportName { get; set; }
        public int ProjectId { get; set; }
        [Display(Name = "Year Number")]
        public int? YearNo { get; set; }
        [StringLength(10), Display(Name = "Year Name")]
        public string YearName { get; set; }
    }
}
