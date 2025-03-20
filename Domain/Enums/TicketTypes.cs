using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum TicketTypes
    {
        [Description("Bug")]
        Bug = 1,

        [Description("Task")]
        Task = 2
    }
}
