using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Models
{
    public partial class UserRights
    {
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public bool ShowMenu { get; set; }
        public bool CreateRight { get; set; }
        public bool ViewRight { get; set; }
        public bool EditRight { get; set; }
        public bool DeleteRight { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual Menu Menu { get; set; }
    }
}
