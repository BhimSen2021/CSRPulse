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
        public Process()
        {
            ProcessDocument = new HashSet<ProcessDocument>();
            ProcessSetup = new HashSet<ProcessSetup>();
        }

        [Key]
        public int ProcessId { get; set; }
        [Required]
        [StringLength(500)]
        public string ProcessName { get; set; }
        public int? FinalStatus { get; set; }
        public bool IsActive { get; set; }
        public bool Document { get; set; }
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
        [InverseProperty(nameof(User.ProcessCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.ProcessCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ProcessUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.ProcessUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("Process")]
        public virtual ICollection<ProcessDocument> ProcessDocument { get; set; }
        [InverseProperty("Process")]
        public virtual ICollection<ProcessSetup> ProcessSetup { get; set; }
    }
}
