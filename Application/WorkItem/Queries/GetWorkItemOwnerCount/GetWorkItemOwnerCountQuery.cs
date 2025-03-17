using MediatR;

namespace Application.WorkItem.Queries.GetWorkItemOwnerCount
{
    public class GetWorkItemOwnerCountQuery : IRequest<List<Array>>
    {
    }
}
