using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectNarrativeQuestion : BaseModel
    {
        public int ProjectQuestionId { get; set; }
        public int ProjectId { get; set; }
        public int QuestionId { get; set; }

        [Required, StringLength(500)]
        public string Question { get; set; }
        public int ProcessId { get; set; }
        public bool? CommentRequire { get; set; }
        [Required, StringLength(8)]
        public string QuestionType { get; set; }
        public int? ContentLimit { get; set; }
        public int? OrderNo { get; set; }
    }
}
