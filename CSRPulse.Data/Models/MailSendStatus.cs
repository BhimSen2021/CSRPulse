using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class MailSendStatus
    {
        [Key]
        [Column("MSStatusId")]
        public int MsstatusId { get; set; }
        public int? CustomerId { get; set; }
        [Required]
        [StringLength(100)]
        public string ToEmails { get; set; }
        [StringLength(200)]
        public string CcEmails { get; set; }
        [StringLength(100)]
        public string BccEmails { get; set; }
        public int? SubjectId { get; set; }
        [StringLength(2000)]
        public string MailContent { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SentOn { get; set; }
        [Required]
        public bool? Status { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("MailSendStatus")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(SubjectId))]
        [InverseProperty(nameof(MailSubject.MailSendStatus))]
        public virtual MailSubject Subject { get; set; }
    }
}
