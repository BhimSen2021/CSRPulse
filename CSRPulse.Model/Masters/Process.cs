using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Process : BaseModel
    {
        [Key]
        public int ProcessId { get; set; }
        [Required]
        [StringLength(500), MinLength(2, ErrorMessage = "Process name should be contain atleast 2 characters.")]
        [Display(Name = "Process Name")]
        public string ProcessName { get; set; }
        [Display(Name = "Document")]
        public bool Document { get; set; }
        [Display(Name = "Final Status")]
        public int? FinalStatus { get; set; }
        
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }
    }
}
