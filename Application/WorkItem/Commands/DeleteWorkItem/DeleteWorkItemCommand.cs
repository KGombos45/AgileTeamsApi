
using MediatR;

namespace Application.WorkItem.Commands.DeleteWorkItem
{
    public class DeleteWorkItemCommand : IRequest<Unit>
    {
        public string WorkItemId { get; set; }
    }
}
