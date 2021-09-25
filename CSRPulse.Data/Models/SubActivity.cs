using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class SubActivity
    {
        public SubActivity()
        {
            Indicator = new HashSet<Indicator>();
        }

        [Key]
        public int SubActivityId { get; set; }
        [Required]
        [StringLength(200)]
        public string SubActivityName { get; set; }
        [StringLength(3)]
        public string SubActivityCode { get; set; }
        public int ActivityId { get; set; }
        public int ThemeId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
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

        [ForeignKey(nameof(ActivityId))]
        [InverseProperty("SubActivity")]
        public virtual Activity Activity { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.SubActivityCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.SubActivityCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(ThemeId))]
        [InverseProperty("SubActivity")]
        public virtual Theme Theme { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.SubActivityUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.SubActivityUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("SubActivity")]
        public virtual ICollection<Indicator> Indicator { get; set; }
    }
}
