using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.WorkItem.Queries.GetPriorities
{
    public class GetPrioritiesQueryHandler : IRequestHandler<GetPrioritiesQuery, List<WorkItemPriority>>
    {
        private readonly IWorkItemService _workItemService;
        public GetPrioritiesQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<WorkItemPriority>> Handle(GetPrioritiesQuery request, CancellationToken cancellationToken)
        {
            var types = await _workItemService.GetPriorities();
            return types;
        }
    }
}
