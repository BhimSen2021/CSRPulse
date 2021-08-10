using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class AuditReviewModule:BaseModel
    {
        [Key]
        public int ModuleId { get; set; }
        [Required,Display(Name ="Module Name")]
        [StringLength(256, MinimumLength = 2, ErrorMessage = "Module name should be contain atleast 2 characters.")]
        public string Module { get; set; }
        
        [Required]        
        public int Sequence { get; set; }

        [Display(Name ="Active")]
        public bool? IsActiveFilter { get; set; }
    }
}
