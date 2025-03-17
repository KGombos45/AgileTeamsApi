using MediatR;

namespace Application.Ticket.Commands.CreateTicket
{
    public class CreateTicketCommand : IRequest<Unit>
    {
        public Domain.Entities.AgileTeams.Ticket Ticket { get; set; }
    }
}
