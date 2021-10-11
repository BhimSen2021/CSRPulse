using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRPulse.Model;

namespace CSRPulse.Models
{
    public class ProjectTeamModel
    {
        public int ProjectId { get; set; }
        public List<ProjectTeam> ProjectTeams { get; set; }
    }
}
