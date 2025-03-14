using Application.Common.Interfaces;
using MediatR;

namespace Application.Ticket.Queries.GetTicketOwnerCount
{
    public class GetTicketOwnerCountQueryHandler : IRequestHandler<GetTicketOwnerCountQuery, List<Array>>
    {
        private readonly ITicketService _ticketService;
        public GetTicketOwnerCountQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<List<Array>> Handle(GetTicketOwnerCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketService.GetTicketOwnerCount();

            return response;
        }
    }   
    
}
