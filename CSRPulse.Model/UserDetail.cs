using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Model
{
    public class UserDetail:BaseModel
    {

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int DepartmentID { get; set; }
        public int UserTypeID { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string PresentAddress { get; set; }
        public string DepartmentName { get; set; }
        public int GenderID { get; set; }
        public bool IsMaintenance { get; set; }
        public Nullable<System.DateTime> MaintenanceDateTime { get; set; }
        public List<UserRight> userMenuRights { set; get; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

    }
}
