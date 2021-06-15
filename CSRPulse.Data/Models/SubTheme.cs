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

        [ForeignKey(nameof(ThemeId))]
        [InverseProperty("SubTheme")]
        public virtual Theme Theme { get; set; }
        [InverseProperty("SubTheme")]
        public virtual ICollection<Indicator> Indicator { get; set; }
    }
}
