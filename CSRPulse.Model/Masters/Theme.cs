﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Theme : BaseModel
    {
        [Key]
        public int ThemeId { get; set; }
        [Required, Display(Name = "Theme Name")]
        [StringLength(50)]
        public string ThemeName { get; set; }
        [StringLength(3), Display(Name = "Theme Code")]
        public string ThemeCode { get; set; }
    }
}
