using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectOtherSource
    {
        [Key]
        public int ProjectOtherSourceId { get; set; }
        public int ProjectId { get; set; }
        public int? SourceId { get; set; }
        public decimal? Amount { get; set; }
        public int RevisionNo { get; set; }
        public string Per { get; set; }
    }
}
