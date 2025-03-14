using Application.Common.Interfaces;
using MediatR;

namespace Application.Ticket.Queries.GetTicketTypeCount
{
    public class GetTicketTypeCountQueryyHandler : IRequestHandler<GetTicketTypeCountQuery, List<Array>>
    {
        private readonly ITicketService _ticketService;
        public GetTicketTypeCountQueryyHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<List<Array>> Handle(GetTicketTypeCountQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketService.GetTicketTypeCount();

            return response;
        }
    }   
    
}
