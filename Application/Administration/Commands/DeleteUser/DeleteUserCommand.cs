using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Administration.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
    }
}
