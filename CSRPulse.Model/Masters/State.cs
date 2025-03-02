﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class State : BaseModel
    {
        public int StateId { get; set; }
        [Required]
        [StringLength(200), MinLength(2, ErrorMessage = "State name should be contain atleast 2 characters.")]
        [Display(Name = "State Name")]
        public string StateName { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State code must be contain 2 digits number")]
        [Display(Name = "State Code")]
        public string StateCode { get; set; }
        [StringLength(4)]
        [Display(Name = "Short Name")]
        public string StateShort { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public bool RecordExist { get; set; }
        [Display(Name = "Active")]
        public bool? IsActiveFilter { get; set; }
    }
}
