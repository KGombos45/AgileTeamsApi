using Application.Common.Models;
using MediatR;

namespace Application.Ticket.Queries.GetUserTickets
{
    public class GetUserTicketsQuery : IRequest<List<TicketDto>>
    {
        public string UserId { get; set; }
    }
}
