using MediatR;

namespace Application.WorkItem.Commands.UpdateWorkItem
{
    public class UpdateWorkItemCommand : IRequest<Unit>
    {
        public Domain.Entities.AgileTeams.WorkItem WorkItem { get; set; }
    }
}
