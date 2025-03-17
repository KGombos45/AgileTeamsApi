
using Domain.Entities.AgileTeams;


namespace Application.Common.Interfaces
{
    using WorkItem = Domain.Entities.AgileTeams.WorkItem;
    public interface IWorkItemService
    {
        public Task Create(WorkItem workItem);
        public Task Update(WorkItem workItem);
        public Task Delete(string workItemId);
        public Task CreateComment(WorkItemComment workItemComment);
        public Task<List<WorkItemStatus>> GetStatuses();
        public Task<List<WorkItemType>> GetTypes();
        public Task<List<WorkItemPriority>> GetPriorities();
        public Task<List<WorkItem>> GetWorkItems();
        public Task<List<Array>> GetWorkItemStatusCount();
        public Task<List<Array>> GetWorkItemPriorityCount();
        public Task<List<Array>> GetWorkItemOwnerCount();
        public Task<List<WorkItem>> GetUserWorkItems(string userId);
    }
}
