using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;

namespace Application.Common.Models
{
    public class ProjectDto
    {
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public virtual IEnumerable<ProjectWorkItemDto> WorkItems { get; set; }
    }

    public class ProjectWorkItemDto
    {
        public string WorkItemID { get; set; }
        public string WorkItemName { get; set; }
        public string WorkItemDescription { get; set; }
        public string WorkItemProjectID { get; set; }
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
}
