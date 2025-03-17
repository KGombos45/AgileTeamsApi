

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
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<IdentityResult>> Register([FromBody] UserRegistrationDto request)
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

        [HttpGet]
        [Authorize]
        //Get : /api/UserAccount
        public async Task<ActionResult<ApplicationUser>> GetUserAccount()
        {
            var response = await Mediator.Send(new GetUserAccountQuery());

            return Ok(response);

        }

        [HttpPut("{id}")]
        //Put : /api/UserAccount/id
        public async Task<ActionResult<IdentityResult>> UpdateUserAccount(string id, ApplicationUser user)
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
