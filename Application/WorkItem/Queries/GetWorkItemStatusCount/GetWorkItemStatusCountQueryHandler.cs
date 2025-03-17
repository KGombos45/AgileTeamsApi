using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItemStatusCount
{
    public class GetWorkItemStatusCountQueryHandler : IRequestHandler<GetWorkItemStatusCountQuery, List<Array>>
    {
        private readonly IWorkItemService _workItemService;
        public GetWorkItemStatusCountQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<Array>> Handle(GetWorkItemStatusCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _workItemService.GetWorkItemStatusCount();

            return response;
        }
    }   
    
}
