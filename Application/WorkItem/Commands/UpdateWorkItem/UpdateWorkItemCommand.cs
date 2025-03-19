using Application.Common.Models;
using MediatR;

namespace Application.WorkItem.Commands.UpdateWorkItem
{
    public class UpdateWorkItemCommand : IRequest<Unit>
    {
        public UpdateWorkItemDto WorkItem { get; set; }
    }
}
