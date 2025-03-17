using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Project.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<Unit>
    {
        public Domain.Entities.AgileTeams.Project Project { get; set; }
    }
}
