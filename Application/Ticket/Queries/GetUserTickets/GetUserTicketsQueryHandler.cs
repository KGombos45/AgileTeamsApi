using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Ticket.Queries.GetUserTickets
{
    using Ticket = Domain.Entities.AgileTeams.Ticket;
    public class GetUserTicketsQueryHandler : IRequestHandler<GetUserTicketsQuery, List<TicketDto>>
    {
        private readonly ITicketService _ticketService;
        public GetUserTicketsQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<List<TicketDto>> Handle(GetUserTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketService.GetUserTickets(request.UserId);

            return tickets;
        }
    }
}
