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
        [Required, Display(Name = "Sub Activity Name")]
        [StringLength(200, MinimumLength =2,  ErrorMessage = "Sub Activity name should be contain atleast 2 characters.")]
        public string SubActivityName { get; set; }
        [StringLength(3), Display(Name = "Sub Activity Code")]
        public string SubActivityCode { get; set; }
        [Required, Display(Name = "Activity Name")]
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }

        [Required, Display(Name = "Theme Name")]
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }
    }
}
