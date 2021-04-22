using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model.Admin
{
    public class UserTypeModel : BaseModel
    {
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }

        public UserModel userModel { get; set; }
    }
}
