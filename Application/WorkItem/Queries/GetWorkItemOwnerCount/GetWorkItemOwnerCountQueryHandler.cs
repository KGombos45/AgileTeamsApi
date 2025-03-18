using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItemOwnerCount
{
    public class GetWorkItemOwnerCountQueryHandler : IRequestHandler<GetWorkItemOwnerCountQuery, List<CountResponse>>
    {
        private readonly IWorkItemService _workItemService;
        public GetWorkItemOwnerCountQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<CountResponse>> Handle(GetWorkItemOwnerCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _workItemService.GetWorkItemOwnerCount();

            return response;
        }
    }   
    
}
