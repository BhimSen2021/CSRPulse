using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class MailProcess
    {
        public MailProcess()
        {
            MailSubject = new HashSet<MailSubject>();
        }

        [Key]
        [Column("MailProcessID")]
        public int MailProcessId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProcessName { get; set; }

        [InverseProperty("MailProcess")]
        public virtual ICollection<MailSubject> MailSubject { get; set; }
    }
}
