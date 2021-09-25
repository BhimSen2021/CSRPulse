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
        [InverseProperty(nameof(User.MailSubjectCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.MailSubjectCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(MailProcessId))]
        [InverseProperty("MailSubject")]
        public virtual MailProcess MailProcess { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.MailSubjectUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.MailSubjectUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<MailSendStatus> MailSendStatus { get; set; }
    }
}
