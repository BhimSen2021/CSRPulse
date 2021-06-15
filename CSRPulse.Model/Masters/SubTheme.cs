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

        [Required, Display(Name = "SubTheme Name")]
        [StringLength(200)]
        public string SubThemeName { get; set; }
        [StringLength(3), Display(Name = "SubTheme Code")]
        public string SubThemeCode { get; set; }
    }
}
