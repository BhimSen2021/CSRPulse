using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model.Admin
{
   public class UserModel:BaseModel
    {
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string ImageName { get; set; }
        public int? DefaultMenuId { get; set; }
        public bool IsActive { get; set; }
    }
}
