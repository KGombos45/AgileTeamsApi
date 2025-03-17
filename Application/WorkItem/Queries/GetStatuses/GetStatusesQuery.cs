using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Workitem.Queries.GetStatuses
{
    public class GetStatusesQuery : IRequest<List<WorkItemStatus>>
    {
    }
}
