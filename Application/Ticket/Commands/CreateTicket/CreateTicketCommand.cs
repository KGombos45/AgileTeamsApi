using Application.Common.Models;
using MediatR;

namespace Application.Ticket.Commands.CreateTicket
{
    public class CreateTicketCommand : IRequest<Unit>
    {
        public CreateTicketDto Ticket { get; set; }
    }
}
