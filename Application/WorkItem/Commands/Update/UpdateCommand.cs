using MediatR;

namespace Application.WorkItem.Commands.Update
{
    public class UpdateCommand : IRequest<Unit>
    {
        public Domain.Entities.AgileTeams.WorkItem WorkItem { get; set; }
    }
}
