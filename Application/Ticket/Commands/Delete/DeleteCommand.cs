
using MediatR;

namespace Application.Ticket.Commands.Delete
{
    public class DeleteCommand : IRequest<Unit>
    {
        public string TicketId { get; set; }
    }
}
