using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;
using Domain.Enums;

namespace Application.Common.Models
{
    public class CreateWorkItemDto
    {
        public string WorkItemName { get; set; }
        public string WorkItemDescription { get; set; }
        public string WorkItemProjectID { get; set; }
        public WorkItemStatuses WorkItemStatusID { get; set; }
        public WorkItemTypes WorkItemTypeID { get; set; }
        public Priorities WorkItemPriorityID { get; set; }
        public string WorkItemOwnerID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? TargetEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
    }
}
