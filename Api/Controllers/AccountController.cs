

using Api.Common;
using Application.Common.Models;
using Application.Account.Commands.Register;
using Application.Accounts.Commands.Login;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;
using Application.Account.Queries.GetUserAccount;
using Microsoft.EntityFrameworkCore;
using Application.Account.Commands.UpdateUserAccount;

namespace Api.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        [HttpPost("register", Name = nameof(Register))]
        public async Task<ActionResult<IdentityResult>> Register([FromBody] RegistrationRequest request)
        {
            var command = new RegisterCommand
            {
                User = request,
            };

            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("login", Name = nameof(Login))]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest request)
        {
            var command = new LoginCommand
            {
                Password = request.Password,
                UserName = request.UserName,
            };

            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("userAccount")]
        [Authorize]
        public async Task<ActionResult<ApplicationUserDto>> GetUserAccount()
        {
            var response = await Mediator.Send(new GetUserAccountQuery());

            return Ok(response);

        }

        [HttpPut("update", Name = nameof(UpdateUserAccount))]
        public async Task<ActionResult<IdentityResult>> UpdateUserAccount([FromBody] ApplicationUserDto user)
        {
            var command = new UpdateUserAccountCommand
            {
                User = user
            };

            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
