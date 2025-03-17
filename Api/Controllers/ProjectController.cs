using Api.Common;
using Application.Project.Commands.Create;
using Application.Project.Commands.Delete;
using Application.Project.Commands.Update;
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
        [HttpPost("create", Name = nameof(Create))]
        public async Task<ActionResult> Create([FromBody] Project project)
        {
            var command = new CreateCommand
            {
                Project = project
            };

            await Mediator.Send(command);

            return Ok();
        }


        [HttpPut("update", Name = nameof(Update))]
        public async Task<ActionResult> Update([FromBody] Project project)
        {
            var command = new UpdateCommand
            {
                Project = project
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("{projectId}delete", Name = nameof(Delete))]
        public async Task<ActionResult> Delete([FromRoute] string projectId)
        {
            var command = new DeleteCommand
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
