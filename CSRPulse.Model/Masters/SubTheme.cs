using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class SubTheme : BaseModel
    {
        [Key]
        public int SubThemeId { get; set; }

        [Required, Display(Name = "Theme Name")]
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }

        [Required, Display(Name = "Sub Theme Name")]
        [StringLength(200, MinimumLength =2, ErrorMessage = "Sub Theme name should be contain atleast 2 characters.")]
        public string SubThemeName { get; set; }
        [StringLength(3), Display(Name = "Sub Theme Code")]
        public string SubThemeCode { get; set; }
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }
    }
}
