using System;
using System.Collections.Generic;
using CSRPulse.Model;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Models
{
    public class NarrativeModel
    {
        public int ProjectId { get; set; }
        public int ProcessId { get; set; }
        public List<NarrativeQuestion> narratives { get; set; }
    }
}
