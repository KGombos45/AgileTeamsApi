using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Ticket.Queries.GetTickets
{
    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, List<TicketDto>>
    {
        private readonly ITicketService _ticketService;
        public GetTicketsQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<List<TicketDto>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketService.GetTickets();
            return tickets;
        }
    }
}
