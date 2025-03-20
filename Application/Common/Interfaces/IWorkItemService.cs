
using Application.Common.Models;
using Domain.Entities.AgileTeams;


namespace Application.Common.Interfaces
{
    using WorkItem = Domain.Entities.AgileTeams.WorkItem;
    public interface IWorkItemService
    {
        public Task Create(CreateWorkItemDto workItem);
        public Task Update(UpdateWorkItemDto workItem);
        public Task Delete(string workItemId);
        public Task CreateComment(CommentRequest comment);
        public Task<List<WorkItemStatus>> GetStatuses();
        public Task<List<WorkItemType>> GetTypes();
        public Task<List<WorkItemPriority>> GetPriorities();
        public Task<List<WorkItemDto>> GetWorkItems();
        public Task<List<CountResponse>> GetWorkItemStatusCount();
        public Task<List<CountResponse>> GetWorkItemPriorityCount();
        public Task<List<CountResponse>> GetWorkItemOwnerCount();
        public Task<List<WorkItemDto>> GetUserWorkItems(string userId);
    }
}
