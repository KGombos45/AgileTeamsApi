using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Project.Queries.GetProjects
{
    public class GetProjectsQuery : IRequest<List<Domain.Entities.AgileTeams.Project>>
    {

    }
}
