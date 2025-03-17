using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Ticket.Queries.GetTicketTypes
{
    public class GetTicketTypesQueryHandler : IRequestHandler<GetTicketTypesQuery, List<TicketType>>
    {
        private readonly ITicketService _ticketService;
        public GetTicketTypesQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<List<TicketType>> Handle(GetTicketTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _ticketService.GetTypes();
            return types;
        }
    }
}
