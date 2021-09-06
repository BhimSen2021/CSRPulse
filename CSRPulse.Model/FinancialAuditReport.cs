using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class FinancialAuditReport:BaseModel
    {
        public int FareportId { get; set; }
        [Display(Name = "NGO Organization")]
        [Required(ErrorMessage = "NGO Organization is required")]
        public int? NGOId { get; set; }
        [Display(Name = "Project")]
        [Required(ErrorMessage = "Project is required")]
        public int? ProjectId { get; set; }
        [Display(Name = "Auditor Consulting Firm")]
        [Required(ErrorMessage = "Auditor Consulting Firm is required")]
        public int? AuditorId { get; set; }
        [Display(Name = "Auditor Checker")]
        [Required(ErrorMessage = "Auditor Checker is required")]
        public int? AuditCheckerId { get; set; }
        [Display(Name = "Auditor Maker")]
        [Required(ErrorMessage = "Auditor Maker is required")]
        public int? AuditMakerId { get; set; }
        [Display(Name = "Program Manager")]
        [Required(ErrorMessage = "Program Manager is required")]
        public int? ProgramManagerId { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Audit Date")]
        public DateTime? AuditDate { get; set; }
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }
    }
}
