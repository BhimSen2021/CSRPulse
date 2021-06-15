using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Indicator : BaseModel
    {
        [Key]
        public int IndicatorId { get; set; }
        [Required, Display(Name = "Theme Name")]
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }

        [Display(Name = "SubTheme Name")]
        public int? SubThemeId { get; set; }
        public string SubThemeName { get; set; }

        [Display(Name = "Activity Name")]
        public int? ActivityId { get; set; }
        public string ActivityName { get; set; }

        [Display(Name = "SubActivity Name")]
        public int? SubActivityId { get; set; }
        public string SubActivityName { get; set; }

        [Required, Display(Name = "Unit Of Measurement")]
        public int UomId { get; set; }
        public string UnitName { get; set; }

        [Required, Display(Name = "Indicator Name")]
        [StringLength(500)]
        public string IndicatorName { get; set; }

        [Required, Display(Name = "Indicator Short Name")]
        [StringLength(60)]
        public string IndicatorShortName { get; set; }

        [StringLength(50), Display(Name = "Indicator Code")]
        public string IndicatorCode { get; set; }
        public string Description { get; set; }

        [Required, Display(Name = "Indicator Type")]
        public int IndicatorType { get; set; }
        [Required, Display(Name = "Response Type")]
        public int ResponseType { get; set; }
        [Required, Display(Name = "Frequency Of Reporting")]
        public int FrequencyOfReporting { get; set; }
        [Display(Name = "Is Cumulative")]
        public bool IsCumulative { get; set; }
        [Display(Name = "Key Indicator")]
        public bool KeyIndicator { get; set; }
        public bool IsFormula { get; set; }
        [StringLength(2000)]
        public string IsFormulaText { get; set; }
        [StringLength(2000)]
        public string IsFormulaValue { get; set; }
    }
}
