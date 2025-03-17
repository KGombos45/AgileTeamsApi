using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.WorkItem.Queries.GetTypes
{
    public class GetTypesQuery : IRequest<List<WorkItemType>>
    {
    }
}
