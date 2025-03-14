using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Account.Queries.GetUserAccount
{
    public class GetUserAccountQuery : IRequest<ApplicationUser>
    {
    }
}
