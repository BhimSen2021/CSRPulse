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
        [InverseProperty(nameof(User.MailProcessCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.MailProcessCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.MailProcessUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.MailProcessUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("MailProcess")]
        public virtual ICollection<MailSubject> MailSubject { get; set; }
    }
}
