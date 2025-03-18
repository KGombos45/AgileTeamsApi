using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Ticket.Queries.GetTicketOwnerCount
{
    public class GetTicketOwnerCountQueryHandler : IRequestHandler<GetTicketOwnerCountQuery, List<CountResponse>>
    {
        private readonly ITicketService _ticketService;
        public GetTicketOwnerCountQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<List<CountResponse>> Handle(GetTicketOwnerCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketService.GetTicketOwnerCount();

            return response;
        }
    }   
    
}
