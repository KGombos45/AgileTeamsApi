using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Ticket.Queries.GetTickets
{
    public class GetTicketsQuery : IRequest<List<Domain.Entities.AgileTeams.Ticket>>
    {
    }
}
