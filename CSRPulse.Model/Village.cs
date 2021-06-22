using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
   public class Village:BaseModel
    {
        public int VillageId { get; set; }
        [Display(Name ="Block")]
        [Required]
        [Range(1, Int64.MaxValue)]
        public int BlockId { get; set; }
        [Display(Name = "District")]
        [Required]
        [Range(1, Int64.MaxValue)]
        public int DistrictId { get; set; }
        [Display(Name = "State")]
        [Required]
        [Range(1, Int64.MaxValue)]
        public int StateId { get; set; }
        [Required]
        [StringLength(50, MinimumLength =2,ErrorMessage = "Village name should be contain atleast 2 characters.")]
        [Display(Name = "Village")]
        public string VillageName { get; set; }
        [Display(Name = "Village Code")]
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Village code must be contain 6 digits number")]
        public string VillageCode { get; set; }
        public Common.EnumLocationType LocationType { get; set; }       
        public bool RecordExist { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
    }
}
