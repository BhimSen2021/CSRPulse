using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class DesignationHistory
    {
        [Key]
        public int DesignationHistoryId { get; set; }
        public int DesignationId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Formdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Todate { get; set; }
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
        [InverseProperty("DesignationHistoryCreatedByNavigation")]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.DesignationHistoryCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(DesignationId))]
        [InverseProperty("DesignationHistory")]
        public virtual Designation Designation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty("DesignationHistoryUpdatedByNavigation")]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.DesignationHistoryUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("DesignationHistoryUser")]
        public virtual User User { get; set; }
    }
}
