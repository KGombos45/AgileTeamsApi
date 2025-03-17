using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItemTypes
{
    public class GetWorkItemTypesQueryHandler : IRequestHandler<GetWorkItemTypesQuery, List<WorkItemType>>
    {
        private readonly IWorkItemService _workItemService;
        public GetWorkItemTypesQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<WorkItemType>> Handle(GetWorkItemTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _workItemService.GetTypes();

            return types;
        }
    }
}
