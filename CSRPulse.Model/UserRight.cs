using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRPulse.Model
{
    public class UserRight:BaseModel
    {
        
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool ShowMenu { get; set; }
        public bool CreateRight { get; set; }
        public bool EditRight { get; set; }
        public bool ViewRight { get; set; }
        public bool DeleteRight { get; set; }
        public Menu menu { set; get; }
        public string HelpText { set; get; }
    }
}
