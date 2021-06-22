using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Block : BaseModel
    {
        public int BlockId { get; set; }
        [Required]
        [Range(1, Int64.MaxValue)]
        [Display(Name = "District Name")]
        public int? DistrictId { get; set; }
        [Required]
        [Range(1, Int64.MaxValue)]
        [Display(Name = "State Name")]
        public int? StateId { get; set; }
        [Required]
        [StringLength(50, MinimumLength =2,ErrorMessage = "Block name should be contain atleast 2 characters.")]
        [Display(Name = "Block Name")]
        public string BlockName { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Block code must be contain 5 digits number")]
        [Display(Name = "Block Code")]
        public string BlockCode { get; set; }
        public int BlockType { get; set; }
        public bool RecordExist { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
    }
}
