using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class District : BaseModel
    {
        public int DistrictId { get; set; }
        [Required(ErrorMessage = "Please select State")]
        [Range(1, Int64.MaxValue)]
        [Display(Name = "State Name")]
        public int? StateId { get; set; }
        [Required]
        [Display(Name = "District Name")]
        [StringLength(200), MinLength(2, ErrorMessage = "District name should be contain atleast 2 characters.")]
        public string DistrictName { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "District code must be contain 3 digits number")]
        [Display(Name = "District Code")]
        public string DistrictCode { get; set; }
        [StringLength(4)]
        [Display(Name = "Short Name")]
        public string DistrictShort { get; set; }
        public bool RecordExist { get; set; }
        public string StateName { get; set; }
    }
}
