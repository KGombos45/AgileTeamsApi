using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Entities.AgileTeams;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account.Commands.UpdateUserAccount
{
    public class UpdateUserAccountCommand : IRequest<IdentityResult>
    {
        public ApplicationUserDto User { get; set; }
    }
}
