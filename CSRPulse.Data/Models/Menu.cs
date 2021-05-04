using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CSRPulse.Data.Models
{
    public partial class Menu
    {
        public Menu()
        {
            UserRights = new HashSet<UserRights>();
        }

        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int? ParentMenuId { get; set; }
        public int? SequenceNo { get; set; }
        public string Url { get; set; }
        public string IconClass { get; set; }
        public bool? IsActive { get; set; }
        public bool ActiveOnMobile { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Help { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<UserRights> UserRights { get; set; }
    }
}
