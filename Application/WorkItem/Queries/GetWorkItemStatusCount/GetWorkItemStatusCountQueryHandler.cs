using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItemStatusCount
{
    public class GetWorkItemStatusCountQueryHandler : IRequestHandler<GetWorkItemStatusCountQuery, List<CountResponse>>
    {
        private readonly IWorkItemService _workItemService;
        public GetWorkItemStatusCountQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<CountResponse>> Handle(GetWorkItemStatusCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _workItemService.GetWorkItemStatusCount();

            return response;
        }
    }   
    
}
