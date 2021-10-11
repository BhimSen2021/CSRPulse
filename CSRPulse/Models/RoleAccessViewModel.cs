using CSRPulse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSRPulse.Models
{
    public class RoleAccessViewModel
    {
        public List<RoleAccessRights> menuParentList { set; get; }
        public List<RoleAccessRights> menuChildList { set; get; }
    }
}
