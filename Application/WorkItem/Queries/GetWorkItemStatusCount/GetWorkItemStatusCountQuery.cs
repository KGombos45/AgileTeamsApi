using Application.Common.Models;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItemStatusCount
{
    public class GetWorkItemStatusCountQuery : IRequest<List<CountResponse>>
    {
    }
}
