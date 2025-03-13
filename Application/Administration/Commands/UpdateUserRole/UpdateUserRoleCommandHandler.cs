using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Administration.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, Unit>
    {
        private readonly IAdministrationService _administrationService;

        public UpdateUserRoleCommandHandler(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        public async Task<Unit> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            await _administrationService.UpdateUserRole(request.UserId, request.User);

            return Unit.Value;
        }
    }
}
