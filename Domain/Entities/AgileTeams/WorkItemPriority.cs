using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities.AgileTeams
{
    public class WorkItemPriority
    {
        public Priorities PriorityID { get; set; }
        public string PriorityName { get; set; }
    }
}
