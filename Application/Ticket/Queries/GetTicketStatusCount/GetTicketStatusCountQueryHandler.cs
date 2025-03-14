using Application.Common.Interfaces;
using MediatR;

namespace Application.Ticket.Queries.GetTicketStatusCount
{
    public class GetTicketStatusCountQueryHandler : IRequestHandler<GetTicketStatusCountQuery, List<Array>>
    {
        private readonly ITicketService _ticketService;
        public GetTicketStatusCountQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<List<Array>> Handle(GetTicketStatusCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketService.GetTicketStatusCount();

            return response;
        }
    }   
    
}
