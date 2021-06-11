using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Maintenance
    {
        [Key]
        public int MaintenanceId { get; set; }
        [Display(Name = "Under Maintenance")]
        public bool IsMaintenance { get; set; }

        [Required, DataType(DataType.DateTime), Display(Name = "Start Date Time")]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.DateTime), Display(Name = "End Date Time")]
        public DateTime? EndDateTime { get; set; }

        [Required, StringLength(maximumLength: 2000)]
        public string Message { get; set; } = "Our application is currently undergoing scheduled maintenance.we Should be back shortly. Thisnk you for your patience.";

    }
}
