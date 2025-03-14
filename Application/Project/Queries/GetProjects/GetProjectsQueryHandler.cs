using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Project.Queries.GetProjects
{
    using Project = Domain.Entities.AgileTeams.Project;

    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<Project>>
    {
        private readonly IProjectService _projectService;

        public GetProjectsQueryHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<List<Project>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var response = await _projectService.GetProjects();

            return response;
        }
    }
}
