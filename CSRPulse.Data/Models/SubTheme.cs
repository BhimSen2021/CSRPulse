using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class SubTheme
    {
        public SubTheme()
        {
            Indicator = new HashSet<Indicator>();
            Project = new HashSet<Project>();
        }

        [Key]
        public int SubThemeId { get; set; }
        public int ThemeId { get; set; }
        [Required]
        [StringLength(200)]
        public string SubThemeName { get; set; }
        [StringLength(3)]
        public string SubThemeCode { get; set; }
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
        [InverseProperty(nameof(User.SubThemeCreatedByNavigation))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(CreatedRid))]
        [InverseProperty(nameof(Role.SubThemeCreatedR))]
        public virtual Role CreatedR { get; set; }
        [ForeignKey(nameof(ThemeId))]
        [InverseProperty("SubTheme")]
        public virtual Theme Theme { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.SubThemeUpdatedByNavigation))]
        public virtual User UpdatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedRid))]
        [InverseProperty(nameof(Role.SubThemeUpdatedR))]
        public virtual Role UpdatedR { get; set; }
        [InverseProperty("SubTheme")]
        public virtual ICollection<Indicator> Indicator { get; set; }
        [InverseProperty("SubTheme")]
        public virtual ICollection<Project> Project { get; set; }
    }
}
