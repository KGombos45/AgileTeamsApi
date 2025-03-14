using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Ticket.Queries.GetTypes
{
    public class GetTypesQueryHandler : IRequestHandler<GetTypesQuery, List<TicketType>>
    {
        private readonly ITicketService _ticketService;
        public GetTypesQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<List<TicketType>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _ticketService.GetTypes();
            return types;
        }
    }
}
