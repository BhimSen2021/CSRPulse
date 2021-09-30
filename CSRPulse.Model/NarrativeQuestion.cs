using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class NarrativeQuestion : BaseModel
    {
        public int QuestionId { get; set; }
      
        [Required, StringLength(500),Display(Name = "Section/Question")]
        public string Question { get; set; }
        [Display(Name = "Process")]
        public int ProcessId { get; set; }
        [Display(Name = "Comment Require")]
        public bool CommentRequire { get; set; }
        [Required, StringLength(8), Display(Name = "Type")]
        public string QuestionType { get; set; }
        [Display(Name = "Content Limit")]
        public int? ContentLimit { get; set; }
        [Display(Name = "Sequence No")]
        public int? OrderNo { get; set; }
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }
        public bool AssigneNarrative { get; set; }
    }
}
