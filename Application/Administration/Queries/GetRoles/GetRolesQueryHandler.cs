using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Administration.Queries.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<string>>
    {
        private readonly IAdministrationService _administrationService;

        public GetRolesQueryHandler(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        public async Task<List<string>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _administrationService.GetRoles();

            return roles;
        }
    }
}
