using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Ticket.Queries.GetTicketTypeCount
{
    public class GetTicketTypeCountQueryyHandler : IRequestHandler<GetTicketTypeCountQuery, List<CountResponse>>
    {
        private readonly ITicketService _ticketService;
        public GetTicketTypeCountQueryyHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<List<CountResponse>> Handle(GetTicketTypeCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketService.GetTicketTypeCount();

            return response;
        }
    }   
    
}
