
using MediatR;

namespace Application.WorkItem.Commands.Delete
{
    public class DeleteCommand : IRequest<Unit>
    {
        public string WorkItemId { get; set; }
    }
}
