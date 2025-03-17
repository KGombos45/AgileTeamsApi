using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.WorkItem.Queries.GetTypes
{
    public class GetTypesQueryHandler : IRequestHandler<GetTypesQuery, List<WorkItemType>>
    {
        private readonly IWorkItemService _workItemService;
        public GetTypesQueryHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<List<WorkItemType>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _workItemService.GetTypes();

            return types;
        }
    }
}
