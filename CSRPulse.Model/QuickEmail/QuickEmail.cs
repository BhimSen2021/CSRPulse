
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace CSRPulse.Model
{
    public class QuickEmail
    {
        [Required]
        public string To { get; set; }
                 
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
