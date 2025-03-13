using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Administration.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
