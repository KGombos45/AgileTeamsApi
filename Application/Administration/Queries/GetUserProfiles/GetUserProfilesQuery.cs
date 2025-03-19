using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Administration.Queries.GetUserProfiles
{
    public class GetUserProfilesQuery: IRequest<List<ApplicationUserDto>>
    {

    }
}
