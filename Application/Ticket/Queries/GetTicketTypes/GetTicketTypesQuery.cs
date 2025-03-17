using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Ticket.Queries.GetTicketTypes
{
    public class GetTicketTypesQuery : IRequest<List<TicketType>>
    {
    }
}
