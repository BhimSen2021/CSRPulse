using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CSRPulse.Model
{
    public class FundingSource : BaseModel
    {
        public int SourceId { get; set; }
        public int? Sequence { get; set; }
        [Display(Name = "Source Type")]
        [Required(ErrorMessage = "Source Type is required.")]
        public int? SourceType { get; set; }
        [Display(Name = "Source Name")]
        [Required(ErrorMessage = "Source Name is required.")]
        public string SourceName { get; set; }
        public int? FilterSourceType { get; set; }
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }
    }
}
