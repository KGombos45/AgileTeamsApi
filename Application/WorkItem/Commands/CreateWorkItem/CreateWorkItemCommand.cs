using MediatR;

namespace Application.WorkItem.Commands.CreateWorkItem
{
    public class CreateWorkItemCommand : IRequest<Unit>
    {
        public Domain.Entities.AgileTeams.WorkItem WorkItem { get; set; }
    }
}
