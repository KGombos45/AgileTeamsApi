using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities.AgileTeams
{
    public class WorkItemStatus
    {
        public WorkItemStatuses StatusID { get; set; }
        public string StatusName { get; set; }
    }
}
