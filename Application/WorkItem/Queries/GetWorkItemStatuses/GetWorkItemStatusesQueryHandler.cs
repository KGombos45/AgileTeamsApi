using Application.Common.Interfaces;
using Application.Workitem.Queries.GetWorkItemStatuses;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItemStatuses
{
    public class GetWorkItemStatusesQueryHandler : IRequestHandler<GetWorkItemStatusesQuery, List<WorkItemStatus>>
    {
        private readonly IWorkItemService _workItemService;
        public GetWorkItemStatusesQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        public async Task<List<WorkItemStatus>> Handle(GetWorkItemStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = await _workItemService.GetStatuses();

            return statuses;
        }
    }
}
