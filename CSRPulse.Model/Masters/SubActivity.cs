using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class SubActivity : BaseModel
    {
        [Key]
        public int SubActivityId { get; set; }
        [Required, Display(Name = "SubActivity Name")]
        [StringLength(200)]
        public string SubActivityName { get; set; }
        [StringLength(3), Display(Name = "SubActivity Code")]
        public string SubActivityCode { get; set; }
        [Required, Display(Name = "Activity Name")]
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }

        [Required, Display(Name = "Theme Name")]
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
    }
}
