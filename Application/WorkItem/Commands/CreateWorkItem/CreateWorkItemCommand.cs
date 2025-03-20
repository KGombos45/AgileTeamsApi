using Application.Common.Models;
using MediatR;

namespace Application.WorkItem.Commands.CreateWorkItem
{
    public class CreateWorkItemCommand : IRequest<Unit>
    {
        public CreateWorkItemDto WorkItem { get; set; }
    }
}
