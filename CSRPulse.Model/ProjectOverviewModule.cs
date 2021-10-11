using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectOverviewModule:BaseModel
    {
        public int ProjectQuestionId { get; set; }
        public int ProjectId { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int ProcessId { get; set; }
        public bool? CommentRequire { get; set; }
        public string QuestionType { get; set; }
        public int? ContentLimit { get; set; }
        public int? OrderNo { get; set; }
        public string Projectanswer { get; set; }
        public int ProjectAnswerId { get; set; }

    }
}
