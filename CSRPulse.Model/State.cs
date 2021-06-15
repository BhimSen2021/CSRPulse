using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class State : BaseModel
    {
        public int StateId { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name ="State")]
        public string StateName { get; set; }
        [Required]
        [StringLength(2)]
        [Display(Name = "State Code")]
        public string StateCode { get; set; }
        [StringLength(4)]
        [Display(Name = "Short Name")]
        public string StateShort { get; set; }
     
        public decimal? Longitude { get; set; }
        
        public decimal? Latitude { get; set; }       

        public bool RecordExist { get; set; }
    }
}
