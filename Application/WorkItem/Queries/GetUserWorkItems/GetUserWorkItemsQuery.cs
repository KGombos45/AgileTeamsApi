using MediatR;

namespace Application.WorkItem.Queries.GetUserWorkItems
{
    public class GetUserWorkItemsQuery : IRequest<List<Domain.Entities.AgileTeams.WorkItem>>
    {
        public string UserId { get; set; }
    }
}
