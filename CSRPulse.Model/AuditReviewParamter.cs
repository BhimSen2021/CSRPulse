using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class AuditReviewParamter : BaseModel
    {
        [Key]
        public int ParamterId { get; set; }
        [Display(Name = "Audit Review Module")]
        public int ModuleId { get; set; }
        [Required]
        public string Scope { get; set; }
        [Required, Display(Name = "Review Instruction")]
        public string ReviewInstruction { get; set; }

        [Required]     
        public byte Critical { get; set; }
        public int? Sequence { get; set; }
        [Display(Name = "Reference No")]
        public decimal? ReferenceNo { get; set; }
        [Required, Display(Name = "Maximum Marks")]
        public byte MaximumMarks { get; set; }
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }
    }
}
