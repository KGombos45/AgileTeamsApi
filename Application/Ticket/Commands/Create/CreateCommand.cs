using MediatR;

namespace Application.Ticket.Commands.Create
{
    public class CreateCommand : IRequest<Unit>
    {
        public Domain.Entities.AgileTeams.Ticket Ticket { get; set; }
    }
}
