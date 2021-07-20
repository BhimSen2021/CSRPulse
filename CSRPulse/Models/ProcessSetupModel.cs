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
        public int ProcessId { get; set; }

        public bool Skip { get; set; }
        [Display(Name ="Primary Role")]
        public string PrimaryRole { get; set; }
        [Display(Name ="Secondary Role")]
        public string SecondaryRole { get; set; }
        [Display(Name ="Level Name")]
        public string  LevelName { get; set; }

        public int Sequence { get; set; }
        public int SetupId { get; set; }
    }
}
