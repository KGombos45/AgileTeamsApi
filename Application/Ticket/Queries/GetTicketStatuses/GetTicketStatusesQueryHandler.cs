using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Ticket.Queries.GetTicketStatuses
{
    public class GetTicketStatusesQueryHandler : IRequestHandler<GetTicketStatusesQuery, List<TicketStatus>>
    {
        private readonly ITicketService _ticketService;

        public GetTicketStatusesQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<List<TicketStatus>> Handle(GetTicketStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = await _ticketService.GetStatuses();

            return statuses;
        }
    }
}
