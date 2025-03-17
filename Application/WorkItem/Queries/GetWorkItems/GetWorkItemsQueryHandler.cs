using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItems
{
    using WorkItem = Domain.Entities.AgileTeams.WorkItem;
    public class GetWorkItemsQueryHandler : IRequestHandler<GetWorkItemsQuery, List<WorkItem>>
    {
        private readonly IWorkItemService _workItemService;
        public GetWorkItemsQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<WorkItem>> Handle(GetWorkItemsQuery request, CancellationToken cancellationToken)
        {
            var response  = await _workItemService.GetWorkItems();

            return response;
        }
    }
}
