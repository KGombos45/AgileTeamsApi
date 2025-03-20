using Application.Common.Models;
using MediatR;

namespace Application.Ticket.Commands.UpdateTicket
{
    public class UpdateTicketCommand : IRequest<Unit>
    {
        public UpdateTicketDto Ticket { get; set; }
    }
}
