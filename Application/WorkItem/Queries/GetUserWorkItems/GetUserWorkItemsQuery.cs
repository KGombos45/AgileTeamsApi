using Application.Common.Models;
using MediatR;

namespace Application.WorkItem.Queries.GetUserWorkItems
{
    public class GetUserWorkItemsQuery : IRequest<List<WorkItemDto>>
    {
        public string UserId { get; set; }
    }
}
