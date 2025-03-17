using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Workitem.Queries.GetWorkItemStatuses
{
    public class GetWorkItemStatusesQuery : IRequest<List<WorkItemStatus>>
    {
    }
}
