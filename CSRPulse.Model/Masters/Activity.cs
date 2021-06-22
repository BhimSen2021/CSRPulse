using System.ComponentModel.DataAnnotations;

namespace CSRPulse.Model
{
    public class Activity : BaseModel
    {
        [Key]
        public int ActivityId { get; set; }
        [Required, Display(Name = "Activity Name")]
        [StringLength(200, MinimumLength =2, ErrorMessage = "Activity name should be contain atleast 2 characters.")]
        public string ActivityName { get; set; }
        [StringLength(3), Display(Name = "Activity Code")]
        public string ActivityCode { get; set; }

        [Required, Display(Name = "Theme Name")]
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
    }
}
