using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;

namespace Domain.Entities
{
    public class ProjectWithWorkItems : Project
    {
        public virtual IEnumerable<WorkItem> WorkItems { get; set; }
    }
}
