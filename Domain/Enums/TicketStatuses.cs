using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum TicketStatuses
    {
        [Description("Defined")]
        Defined = 1,

        [Description("In Progress")]
        InProgress = 2,

        [Description("Testing")]
        Testing = 3,

        [Description("Complete")]
        Complete = 4,
    }
}
