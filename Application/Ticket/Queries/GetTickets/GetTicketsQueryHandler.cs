using Application.Common.Interfaces;
using MediatR;

namespace Application.Ticket.Queries.GetTickets
{
    using Ticket = Domain.Entities.AgileTeams.Ticket;
    public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, List<Ticket>>
    {
        private readonly ITicketService _ticketService;
        public GetTicketsQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<List<Ticket>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketService.GetTickets();
            return tickets;
        }
    }
}
