using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.WorkItem.Queries.GetPriorities
{
    public class GetPrioritiesQuery : IRequest<List<WorkItemPriority>>
    {
    }
}
