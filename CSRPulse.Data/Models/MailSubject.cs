using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class MailSubject
    {
        public MailSubject()
        {
            MailSendStatus = new HashSet<MailSendStatus>();
        }

        [Key]
        public int SubjectId { get; set; }
        [Column("MailProcessID")]
        public int MailProcessId { get; set; }
        [StringLength(100)]
        public string Subject { get; set; }
        [StringLength(50)]
        public string ContactUs { get; set; }
        [StringLength(50)]
        public string Signature { get; set; }
        [StringLength(500)]
        public string Disclaimer { get; set; }
        [StringLength(500)]
        public string TermsConditions { get; set; }
        [StringLength(50)]
        public string HeaderImage { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(MailProcessId))]
        [InverseProperty("MailSubject")]
        public virtual MailProcess MailProcess { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<MailSendStatus> MailSendStatus { get; set; }
    }
}
