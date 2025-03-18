using Api.Common;
using Application.Administration.Commands.DeleteUser;
using Application.Administration.Commands.UpdateUserRole;
using Application.Administration.Queries.GetRoles;
using Application.Administration.Queries.GetUserProfiles;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("v{version:apiVersion}/[controller]")]
[ApiController]
public class AdministrationController : ApiControllerBase
{
    [HttpGet("users", Name = nameof(GetUserProfiles))]
    public async Task<ActionResult<List<ApplicationUser>>> GetUserProfiles()
    {
        var response = await Mediator.Send(new GetUserProfilesQuery());

        return Ok(response);

    }

    [HttpPut("{id}/update", Name = nameof(UpdateUserRole))]
    [Authorize(Roles = "Admin")]
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

    [HttpPost("{id}/delete", Name = nameof(DeleteUser))]
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

    [HttpGet("roles", Name = nameof(GetRoles))]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<string>>> GetRoles()
    {
        var response = await Mediator.Send(new GetRolesQuery());

        return Ok(response);

    }
}
