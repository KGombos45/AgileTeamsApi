using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItems
{
    public class GetWorkItemsQueryHandler : IRequestHandler<GetWorkItemsQuery, List<WorkItemDto>>
    {
        private readonly IWorkItemService _workItemService;
        public GetWorkItemsQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<WorkItemDto>> Handle(GetWorkItemsQuery request, CancellationToken cancellationToken)
        {
            var response  = await _workItemService.GetWorkItems();

            return response;
        }
    }
}
