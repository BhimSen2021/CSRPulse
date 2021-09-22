using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CSRPulse.Model
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        [NotMapped]
        public bool AssigneRole { get; set; }
    }
}
