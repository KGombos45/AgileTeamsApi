using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Ticket.Queries.GetTypes
{
    public class GetTypesQuery : IRequest<List<TicketType>>
    {
    }
}
