using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Ticket.Queries.GetStatuses
{
    public class GetStatusesQueryHandler : IRequestHandler<GetStatusesQuery, List<TicketStatus>>
    {
        private readonly ITicketService _ticketService;

        public GetStatusesQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<List<TicketStatus>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = await _ticketService.GetStatuses();

            return statuses;
        }
    }
}
