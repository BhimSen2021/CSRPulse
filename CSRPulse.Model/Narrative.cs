using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
   public class Narrative
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int ProcessId { get; set; }
        public int ReportId { get; set; }
        public bool CommentRequire { get; set; }
        public bool QuestionType { get; set; }
        public int ContentLimit { get; set; }
        public int OrderNo { get; set; }
        public bool AssigneNarrative { get; set; }
    }
}
