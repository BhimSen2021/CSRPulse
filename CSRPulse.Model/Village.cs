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
        [Required(ErrorMessage = "Please select Block")]
        [Range(1, Int64.MaxValue)]
        public int BlockId { get; set; }
        [Display(Name = "District")]
        [Required(ErrorMessage = "Please select District")]
        [Range(1, Int64.MaxValue)]
        public int DistrictId { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "Please select State")]
        [Range(1, Int64.MaxValue)]
        public int StateId { get; set; }
        [Required]
        [StringLength(50)]
        public string VillageName { get; set; }
        [Required]
        [StringLength(6)]
        public string VillageCode { get; set; }
        public Common.EnumLocationType LocationType { get; set; }
        public bool IsActive { get; set; }
        public bool RecordExist { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
    }
}
