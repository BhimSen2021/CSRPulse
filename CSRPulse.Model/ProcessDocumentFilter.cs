using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace CSRPulse.Model
{
    public class ProcessDocumentFilter
    {
        [Display(Name = "Process"), Required(ErrorMessage = "Please select Process")]
        public int ProcessId { get; set; }
    }
}
