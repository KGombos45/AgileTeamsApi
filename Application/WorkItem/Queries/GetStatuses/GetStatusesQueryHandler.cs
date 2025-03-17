using Application.Common.Interfaces;
using Application.Workitem.Queries.GetStatuses;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.WorkItem.Queries.GetStatuses
{
    public class GetStatusesQueryHandler : IRequestHandler<GetStatusesQuery, List<WorkItemStatus>>
    {
        private readonly IWorkItemService _workItemService;
        public GetStatusesQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        public async Task<List<WorkItemStatus>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = await _workItemService.GetStatuses();

            return statuses;
        }
    }
}
