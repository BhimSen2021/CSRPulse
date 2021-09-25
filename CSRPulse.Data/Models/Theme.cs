using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Theme
    {
        public Theme()
        {
            Activity = new HashSet<Activity>();
            Indicator = new HashSet<Indicator>();
            Project = new HashSet<Project>();
            SubActivity = new HashSet<SubActivity>();
            SubTheme = new HashSet<SubTheme>();
        }

        [Key]
        public int ThemeId { get; set; }
        [Required]
        [StringLength(50)]
        public string ThemeName { get; set; }
        [StringLength(3)]
        public string ThemeCode { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
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
        [InverseProperty(nameof(User.ThemeCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.ThemeCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.ThemeUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.ThemeUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("Theme")]
        public virtual ICollection<Activity> Activity { get; set; }
        [InverseProperty("Theme")]
        public virtual ICollection<Indicator> Indicator { get; set; }
        [InverseProperty("Theme")]
        public virtual ICollection<Project> Project { get; set; }
        [InverseProperty("Theme")]
        public virtual ICollection<SubActivity> SubActivity { get; set; }
        [InverseProperty("Theme")]
        public virtual ICollection<SubTheme> SubTheme { get; set; }
    }
}
