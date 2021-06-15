using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CSRPulse.Model
{
    public class Uom : BaseModel
    {
        [Key]
        public int UOMId { get; set; }
        [Required, Display(Name = "Unit Name")]
        [StringLength(50)]
        public string UnitName { get; set; }
        public bool IsActive { get; set; }
    }
}
