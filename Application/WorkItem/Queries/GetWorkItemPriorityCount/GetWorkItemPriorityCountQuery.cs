using Application.Common.Models;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItemTypeCount
{
    public class GetWorkItemPriorityCountQuery : IRequest<List<CountResponse>>
    {
    }
}
