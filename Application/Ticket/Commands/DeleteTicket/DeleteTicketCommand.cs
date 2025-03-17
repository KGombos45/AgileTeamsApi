
using MediatR;

namespace Application.Ticket.Commands.DeleteTicket
{
    public class DeleteTicketCommand : IRequest<Unit>
    {
        public string TicketId { get; set; }
    }
}
