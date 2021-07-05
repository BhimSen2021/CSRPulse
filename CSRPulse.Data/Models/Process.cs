using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Process
    {
        [Key]
        public int ProcessId { get; set; }
        [Required]
        [StringLength(500)]
        public string ProcessName { get; set; }
        public int? FinalStatus { get; set; }
        public bool IsActive { get; set; }
    }
}
