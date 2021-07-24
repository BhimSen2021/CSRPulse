using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CSRPulse.Models
{
    public class ProcessSetupModel
    {        
        [Display(Name = "Process")]
        public int ProcessId { get; set; }
        [Display(Name = "Revision No")]
        public int RevisionNo { get; set; }
        public List<ProcessSetup> processSetupList { get; set; }       
    }
}
