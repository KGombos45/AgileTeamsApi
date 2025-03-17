using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItemTypeCount
{
    public class GetWorkItemPriorityCountQueryyHandler : IRequestHandler<GetWorkItemPriorityCountQuery, List<Array>>
    {
        private readonly IWorkItemService _workItemService;
        public GetWorkItemPriorityCountQueryyHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<Array>> Handle(GetWorkItemPriorityCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _workItemService.GetWorkItemPriorityCount();

            return response;
        }
    }   
    
}
