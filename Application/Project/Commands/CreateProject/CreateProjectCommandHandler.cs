using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Project.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Unit>
    {
        private readonly IProjectService _projectService;
        public CreateProjectCommandHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            await _projectService.CreateProject(request.Project);

            return Unit.Value;
        }
    }
}
