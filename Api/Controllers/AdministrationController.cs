using Api.Common;
using Application.Administration.Commands.DeleteUser;
using Application.Administration.Commands.UpdateUserRole;
using Application.Administration.Queries.GetRoles;
using Application.Administration.Queries.GetUserProfiles;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AdministrationController : ApiControllerBase
{
    [HttpGet]
    [Route("Users")]
    //GET : /api/Administration/Users
    public async Task<ActionResult<List<ApplicationUser>>> GetUserProfiles()
    {
        var response = await Mediator.Send(new GetUserProfilesQuery());

        return Ok(response);

    }

    [HttpPut]
    [Route("UpdateRole/{id}")]
    [Authorize(Roles = "Admin")]
    //PUT : /api/Administration/
    public async Task<IActionResult> UpdateUserRole([FromRoute] string id, [FromBody] ApplicationUser applicationUserRole)
    {
        var command = new UpdateUserRoleCommand
        {
            UserId = id,
            User = applicationUserRole,
        };

        await Mediator.Send(command);

        return Ok();
    }

    [HttpPost]
    [Route("DeleteUser/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser([FromRoute] string id)
    {
        var command = new DeleteUserCommand
        {
            UserId = id,
        };

        await Mediator.Send(command);

        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("Roles")]
    //GET : /api/Administration/Users
    public async Task<IActionResult> GetRoles()
    {
        var response = await Mediator.Send(new GetRolesQuery());

        return Ok(response);

    }
}
