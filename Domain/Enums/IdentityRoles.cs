using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum IdentityRoles
    {
        [Description("Admin")]
        Admin = 1,

        [Description("Developer")]
        Developer = 2,

        [Description("Project Manager")]
        ProjectManager = 3,

        [Description("Submitter")]
        Submitter = 4,

        [Description("Unassigned")]
        Unassigned = 5
    }
}
