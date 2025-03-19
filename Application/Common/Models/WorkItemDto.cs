using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;

namespace Application.Common.Models
{
    public class WorkItemDto
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
        public virtual IEnumerable<WorkItemTicketDto> Tickets { get; set; }
    }

    public class WorkItemCommentDto
    {
        public string CommentID { get; set; }
        public string Comment { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedOn { get; set; }
        public string CommentWorkItemID { get; set; }
    }

    public class WorkItemProjectDto
    {
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class WorkItemTicketDto
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
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
