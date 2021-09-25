using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Activity
    {
        public Activity()
        {
            Indicator = new HashSet<Indicator>();
            SubActivity = new HashSet<SubActivity>();
        }

        [Key]
        public int ActivityId { get; set; }
        [Required]
        [StringLength(200)]
        public string ActivityName { get; set; }
        [StringLength(3)]
        public string ActivityCode { get; set; }
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

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.ActivityCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.ActivityCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(ThemeId))]
        [InverseProperty("Activity")]
        public virtual Theme Theme { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ActivityUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.ActivityUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("Activity")]
        public virtual ICollection<Indicator> Indicator { get; set; }
        [InverseProperty("Activity")]
        public virtual ICollection<SubActivity> SubActivity { get; set; }
    }
}
