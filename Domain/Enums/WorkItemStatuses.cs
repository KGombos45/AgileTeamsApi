using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum WorkItemStatuses
    {
        [Description("Grooming")]
        Grooming = 1,

        [Description("Refining")]
        Refining = 2,

        [Description("Ready")]
        Ready = 3,

        [Description("In Progress")]
        InProgress = 4,

        [Description("Complete")]
        Complete = 5,

        [Description("Closed")]
        Closed = 6
    }
}
