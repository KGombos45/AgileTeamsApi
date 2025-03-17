using MediatR;

namespace Application.WorkItem.Queries.GetWorkItems
{
    public class GetWorkItemsQuery : IRequest<List<Domain.Entities.AgileTeams.WorkItem>>
    {
    }
}
