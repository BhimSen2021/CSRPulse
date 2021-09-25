using System;
using System.Collections.Generic;
using System.Text;

namespace CSRPulse.Model
{
    public class ProjectLocation:BaseModel
    {
        public int LocationId { get; set; }
        public int ProjectId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
    }
}
