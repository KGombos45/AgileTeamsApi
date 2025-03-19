using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;

namespace Application.Common.Models
{
    public class TicketDto
    {
        public string TicketID { get; set; }
        public string TicketName { get; set; }
        public string TicketDescription { get; set; }
        public string CreatedBy { get; set; }
        public int TicketStatusID { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public int TicketTypeID { get; set; }
        public TicketType TicketType { get; set; }
        public string TicketOwnerID { get; set; }
        public ApplicationUserDto TicketOwner { get; set; }
        public string TicketWorkItemID { get; set; }
        public TicketWorkItemDto TicketWorkItem { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class TicketWorkItemDto
    {
        public string WorkItemID { get; set; }
        public string WorkItemName { get; set; }
        public string WorkItemDescription { get; set; }
        public string WorkItemProjectID { get; set; }
        public WorkItemProjectDto Project { get; set; }
        public int WorkItemStatusID { get; set; }
        public WorkItemStatus WorkItemStatus { get; set; }
        public int WorkItemTypeID { get; set; }
        public WorkItemType WorkItemType { get; set; }
        public int WorkItemPriorityID { get; set; }
        public WorkItemPriority WorkItemPriority { get; set; }
        public string WorkItemOwnerID { get; set; }
        public ApplicationUserDto WorkItemOwner { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? TargetEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public virtual IEnumerable<WorkItemCommentDto> Comments { get; set; }
    }
}
