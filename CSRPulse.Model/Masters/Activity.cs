using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Activity : BaseModel
    {
        [Key]
        public int ActivityId { get; set; }
        [Required, Display(Name = "Activity Name")]
        [StringLength(200)]
        public string ActivityName { get; set; }
        [StringLength(3), Display(Name = "Activity Code")]
        public string ActivityCode { get; set; }

        [Required, Display(Name = "Theme Name")]
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
    }
}
