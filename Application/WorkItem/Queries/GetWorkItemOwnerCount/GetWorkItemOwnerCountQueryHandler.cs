using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItemOwnerCount
{
    public class GetWorkItemOwnerCountQueryHandler : IRequestHandler<GetWorkItemOwnerCountQuery, List<Array>>
    {
        private readonly IWorkItemService _workItemService;
        public GetWorkItemOwnerCountQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<Array>> Handle(GetWorkItemOwnerCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _workItemService.GetWorkItemOwnerCount();

            return response;
        }
    }   
    
}
