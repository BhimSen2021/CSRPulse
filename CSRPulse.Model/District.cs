using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
   public class District:BaseModel
    {
        public int DistrictId { get; set; }
        [Required(ErrorMessage = "Please select State")]
        [Range(1,Int64.MaxValue)]
        public int? StateId { get; set; }
        [Required(ErrorMessage = "Please enter District")]
        [Display(Name ="District")]
        [StringLength(200)]
        public string DistrictName { get; set; }
        [Required(ErrorMessage ="Please enter District Code") ]
        [StringLength(3)]
        [Display(Name = "District Code")]
        public string DistrictCode { get; set; }
        [StringLength(4)]
        [Display(Name = "Short Name")]
        public string DistrictShort { get; set; }
        public bool IsActive { get; set; }
        public bool RecordExist { get; set; }
        public string StateName { get; set; }
    }
}
