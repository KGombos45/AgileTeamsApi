using MediatR;

namespace Application.Ticket.Queries.GetTicketTypeCount
{
    public class GetTicketTypeCountQuery : IRequest<List<Array>>
    {
    }
}
