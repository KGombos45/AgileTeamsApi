using MediatR;

namespace Application.Ticket.Commands.Update
{
    public class UpdateCommand : IRequest<Unit>
    {
        public Domain.Entities.AgileTeams.Ticket Ticket { get; set; }
    }
}
