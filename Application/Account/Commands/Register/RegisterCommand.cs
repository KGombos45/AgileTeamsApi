using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account.Commands.Register
{
    public class RegisterCommand : IRequest<IdentityResult>
    {
        public ApplicationUser User { get; set; }
    }
}
