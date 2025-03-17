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
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ApiControllerBase
    {
        [HttpPost]
        [Route("Create")]
        //POST: api/Project/Create
        public async Task<ActionResult> Create(Project project)
        {
            var command = new CreateCommand
            {
                Project = project
            };

            await Mediator.Send(command);

            return Ok();
        }


        [HttpPut]
        [Route("Update")]
        //PUT : /api/Project/Update
        public async Task<ActionResult> Update(Project project)
        {
            var command = new UpdateCommand
            {
                Project = project
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ActionResult> Delete(string projectId)
        {
            var command = new DeleteCommand
            {
                ProjectId = projectId
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("Projects")]
        //GET : /api/Project/Projects
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
           var response = await Mediator.Send(new GetProjectsQuery());

            return Ok(response);
        }
    }
}
