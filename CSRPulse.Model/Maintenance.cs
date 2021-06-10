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
        public bool IsMaintenance { get; set; }
        [Required, DataType(DataType.DateTime), Display(Name = "Start Date Time")]
        public DateTime StartDateTime { get; set; }
        [Required, DataType(DataType.DateTime), Display(Name = "End Date Time")]

        public DateTime EndDateTime { get; set; }
        [Required, StringLength(maximumLength: 2000)]
        public string Message { get; set; }

    }
}
