using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Administration.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public IdentityRoles Role { get; set; }
    }
}
