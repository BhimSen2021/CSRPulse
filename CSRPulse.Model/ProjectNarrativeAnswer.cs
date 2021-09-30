using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectNarrativeAnswer : BaseModel
    {
        public int ProjectAnswerId { get; set; }
        public int ProjectQuestionId { get; set; }

        [Required, StringLength(500)]
        public string Answer { get; set; }
        public int ProcessId { get; set; }
        public int ReportId { get; set; }
    }
}
