using MediatR;

namespace Application.WorkItem.Commands.Create
{
    public class CreateCommand : IRequest<Unit>
    {
        public Domain.Entities.AgileTeams.WorkItem WorkItem { get; set; }
    }
}
