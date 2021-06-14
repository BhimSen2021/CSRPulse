using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
   public class Block:BaseModel
    {
        public int BlockId { get; set; }
        [Required(ErrorMessage = "Please select District")]
        [Range(1, Int64.MaxValue)]
        public int ? DistrictId { get; set; }
        [Required(ErrorMessage = "Please select State")]
        [Range(1, Int64.MaxValue)]
        public int? StateId { get; set; }
        [Required(ErrorMessage = "Please enter Block")]
        [StringLength(50)]
        public string BlockName { get; set; }
        [Required(ErrorMessage = "Please enter Code")]
        [StringLength(5)]
        public string BlockCode { get; set; }
        public int BlockType { get; set; }     
        public bool IsActive { get; set; }
        public bool RecordExist { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
    }
}
