using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class DesignationHistory : BaseModel
    {
        [Key]
        public int DesignationHistoryId { get; set; }
        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public int UserId { get; set; }
        public DateTime Formdate { get; set; }
        public DateTime? Todate { get; set; }
    }
}
