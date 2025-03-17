using MediatR;

namespace Application.Ticket.Commands.UpdateTicket
{
    public class UpdateTicketCommand : IRequest<Unit>
    {
        public Domain.Entities.AgileTeams.Ticket Ticket { get; set; }
    }
}
