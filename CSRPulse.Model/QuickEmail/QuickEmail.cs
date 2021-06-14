
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace CSRPulse.Model
{
    public class QuickEmail
    {
        public QuickEmail()
        {
            ToEmails = new List<string>().ToArray();
            BccEmails = new List<string>().ToArray();
        }
        public List<SelectListItem> ToDropdown { get; set; }
        [Display(Name = "To")]
        public string[] ToEmails { get; set; }

        public List<SelectListItem> BccDropdown { get; set; }
        [Display(Name = "Bcc")]
        public string[] BccEmails { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
