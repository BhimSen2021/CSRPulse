using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
   public class ImportBaseModel
    {
        public string error { set; get; }
        public string warning { set; get; }
        public bool isDuplicatedRow { set; get; }
       
    }
}
