using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Project.Commands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Unit>
    {
        private readonly IProjectService _projectService;
        public UpdateCommandHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            await _projectService.CreateProject(request.Project);

            return Unit.Value;
        }
    }
}
