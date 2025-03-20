using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Domain.Enums;

namespace Domain.Entities.AgileTeams
{
    public class WorkItem
    {
        public string WorkItemID { get; set; }
        public string WorkItemName { get; set; }
        public string WorkItemDescription { get; set; }
        public string WorkItemProjectID { get; set; }
        public Project Project { get; set; }
        public WorkItemStatuses WorkItemStatusID { get; set; }
        public WorkItemStatus WorkItemStatus { get; set; }
        public WorkItemTypes WorkItemTypeID { get; set; }
        public WorkItemType WorkItemType { get; set; }
        public Priorities WorkItemPriorityID { get; set; }
        public WorkItemPriority WorkItemPriority { get; set; }
        public string WorkItemOwnerID { get; set; }
        public ApplicationUser WorkItemOwner { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? TargetEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public ICollection<WorkItemComment> Comments { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
