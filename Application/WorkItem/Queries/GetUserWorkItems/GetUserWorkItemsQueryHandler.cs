using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.WorkItem.Queries.GetUserWorkItems
{
    public class GetUserWorkItemsQueryHandler : IRequestHandler<GetUserWorkItemsQuery, List<WorkItemDto>>
    {
        private readonly IWorkItemService _workItemService;
        public GetUserWorkItemsQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<WorkItemDto>> Handle(GetUserWorkItemsQuery request, CancellationToken cancellationToken)
        {
            var response = await _workItemService.GetUserWorkItems(request.UserId);

            return response;
        }
    }
}
