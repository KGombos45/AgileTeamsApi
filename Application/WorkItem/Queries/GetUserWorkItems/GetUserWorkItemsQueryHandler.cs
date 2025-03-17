using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Queries.GetUserWorkItems
{
    using WorkItem = Domain.Entities.AgileTeams.WorkItem;
    public class GetUserWorkItemsQueryHandler : IRequestHandler<GetUserWorkItemsQuery, List<WorkItem>>
    {
        private readonly IWorkItemService _workItemService;
        public GetUserWorkItemsQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<WorkItem>> Handle(GetUserWorkItemsQuery request, CancellationToken cancellationToken)
        {
            var response = await _workItemService.GetUserWorkItems(request.UserId);

            return response;
        }
    }
}
