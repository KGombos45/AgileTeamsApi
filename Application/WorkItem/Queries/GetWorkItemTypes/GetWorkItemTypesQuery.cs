using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItemTypes
{
    public class GetWorkItemTypesQuery : IRequest<List<WorkItemType>>
    {
    }
}
