using Application.Common.Models;
using MediatR;

namespace Application.WorkItem.Queries.GetWorkItems
{
    public class GetWorkItemsQuery : IRequest<List<WorkItemDto>>
    {
    }
}
