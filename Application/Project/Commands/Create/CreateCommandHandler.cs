using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Project.Commands.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, Unit>
    {
        private readonly IProjectService _projectService;
        public CreateCommandHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            await _projectService.CreateProject(request.Project);

            return Unit.Value;
        }
    }
}
