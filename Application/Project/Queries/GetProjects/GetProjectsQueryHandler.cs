using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Project.Queries.GetProjects
{
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
    {
        private readonly IProjectService _projectService;

        public GetProjectsQueryHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<List<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var response = await _projectService.GetProjects();

            return response;
        }
    }
}
