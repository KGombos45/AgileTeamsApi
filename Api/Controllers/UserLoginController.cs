

using Api.Common;
using Application.Common.Models;
using Application.UserLogin.Commands.Register;
using Application.UserLogin.Commands.Login;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ApiControllerBase
    {
        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public async Task<ActionResult<IdentityResult>> Register([FromBody] ApplicationUser request)
        {
            var command = new RegisterCommand
            {
                User = request,
            };

            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/ApplicationUser/Login
        public async Task<ActionResult<string>> Login([FromBody] ApplicationUserLogin request)
        {
            var command = new LoginCommand
            {
                Password = request.Password,
                UserName = request.UserName,
            };

            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
