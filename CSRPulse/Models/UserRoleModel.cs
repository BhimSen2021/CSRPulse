using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Models
{
    public class UserRoleModel
    {
        public int UserId { get; set; }
        public int SelectedRole { get; set; }      
        public List<UserRole> userRoles { get; set; }
    }
}
