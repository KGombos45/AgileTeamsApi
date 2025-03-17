using Api.Common;
using Application.Project.Commands.CreateProject;
using Application.Project.Commands.DeleteProject;
using Application.Project.Commands.UpdateProject;
using Application.Project.Queries.GetProjects;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Mvc;
using Project = Domain.Entities.AgileTeams.Project;

namespace Api.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProjectController : ApiControllerBase
    {
        [HttpPost("create", Name = nameof(CreateProject))]
        public async Task<ActionResult> CreateProject([FromBody] Project project)
        {
            var command = new CreateProjectCommand
            {
                Project = project
            };

            await Mediator.Send(command);

            return Ok();
        }


        [HttpPut("update", Name = nameof(UpdateProject))]
        public async Task<ActionResult> UpdateProject([FromBody] Project project)
        {
            var command = new UpdateProjectCommand
            {
                Project = project
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("{projectId}delete", Name = nameof(DeleteProject))]
        public async Task<ActionResult> DeleteProject([FromRoute] string projectId)
        {
            var command = new DeleteProjectCommand
            {
                ProjectId = projectId
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet("projects", Name = nameof(GetProjects))]
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
           var response = await Mediator.Send(new GetProjectsQuery());

            return Ok(response);
        }
    }
}
