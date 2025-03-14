using MediatR;

namespace Application.Ticket.Queries.GetUserTickets
{
    public class GetUserTicketsQuery : IRequest<List<Domain.Entities.AgileTeams.Ticket>>
    {
        public string UserId { get; set; }
    }
}
