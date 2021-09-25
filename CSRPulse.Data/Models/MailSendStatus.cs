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
        public string MailContent { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SentOn { get; set; }
        [Required]
        public bool? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column("CreatedRId")]
        public int CreatedRid { get; set; }
        [Required]
        [Column("CreatedRName")]
        [StringLength(256)]
        public string CreatedRname { get; set; }
        [Column("UpdatedRId")]
        public int? UpdatedRid { get; set; }
        [Column("UpdatedRName")]
        [StringLength(256)]
        public string UpdatedRname { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.MailSendStatusCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.MailSendStatusCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("MailSendStatus")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(SubjectId))]
        [InverseProperty(nameof(MailSubject.MailSendStatus))]
        public virtual MailSubject Subject { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.MailSendStatusUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.MailSendStatusUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
