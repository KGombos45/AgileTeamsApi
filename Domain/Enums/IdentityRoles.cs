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
        Admin,
        [Description("Developer")]
        Developer,
        [Description("Project Manager")]
        ProjectManager,
        [Description("Submitter")]
        Submitter,
        [Description("Unassigned")]
        Unassigned
    }
}
