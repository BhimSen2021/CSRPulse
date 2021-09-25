using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class ProcessWorkFlow
    {
        [Key]
        public int WorkflowId { get; set; }
        public int ProcessId { get; set; }
        public int? ReferenceId { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Senddate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Readdate { get; set; }
        public int? Readflag { get; set; }
        [Column(TypeName = "text")]
        public string Scomments { get; set; }
        [StringLength(50)]
        public string Purpose { get; set; }
        public int? StatusId { get; set; }
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
        [InverseProperty(nameof(User.ProcessWorkFlowCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.ProcessWorkFlowCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(ReceiverId))]
        [InverseProperty(nameof(User.ProcessWorkFlowReceiver))]
        public virtual User Receiver { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.ProcessWorkFlowSender))]
        public virtual User Sender { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ProcessWorkFlowUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.ProcessWorkFlowUpdatedR))]
        public virtual Role UpdatedR { get; set; }
    }
}
