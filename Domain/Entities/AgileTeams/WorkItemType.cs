using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities.AgileTeams
{
    public class WorkItemType
    {
        public WorkItemTypes TypeID { get; set; }
        public string TypeName { get; set; }
    }
}
