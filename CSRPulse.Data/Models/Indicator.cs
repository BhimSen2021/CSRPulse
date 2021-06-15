using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Indicator
    {
        [Key]
        public int IndicatorId { get; set; }
        public int ThemeId { get; set; }
        public int? SubThemeId { get; set; }
        public int? ActivityId { get; set; }
        public int? SubActivityId { get; set; }
        [Column("UOMId")]
        public int Uomid { get; set; }
        [Required]
        [StringLength(500)]
        public string IndicatorName { get; set; }
        [Required]
        [StringLength(60)]
        public string IndicatorShortName { get; set; }
        [StringLength(50)]
        public string IndicatorCode { get; set; }
        public string Description { get; set; }
        public int? IndicatorType { get; set; }
        public int? ResponseType { get; set; }
        public int? FrequencyOfReporting { get; set; }
        public bool IsCumulative { get; set; }
        public bool KeyIndicator { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public bool IsFormula { get; set; }
        [StringLength(2000)]
        public string IsFormulaText { get; set; }
        [StringLength(2000)]
        public string IsFormulaValue { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey(nameof(ActivityId))]
        [InverseProperty("Indicator")]
        public virtual Activity Activity { get; set; }
        [ForeignKey(nameof(SubActivityId))]
        [InverseProperty("Indicator")]
        public virtual SubActivity SubActivity { get; set; }
        [ForeignKey(nameof(SubThemeId))]
        [InverseProperty("Indicator")]
        public virtual SubTheme SubTheme { get; set; }
        [ForeignKey(nameof(ThemeId))]
        [InverseProperty("Indicator")]
        public virtual Theme Theme { get; set; }
        [ForeignKey(nameof(Uomid))]
        [InverseProperty("Indicator")]
        public virtual Uom Uom { get; set; }
    }
}
