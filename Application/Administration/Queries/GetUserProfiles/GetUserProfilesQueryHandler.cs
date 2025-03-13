using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Administration.Queries.GetUserProfiles
{
    public class GetUserProfilesQueryHandler : IRequestHandler<GetUserProfilesQuery, List<ApplicationUser>>
    {
        private readonly IAdministrationService _administrationService;

        public GetUserProfilesQueryHandler(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        public async Task<List<ApplicationUser>> Handle(GetUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var users = await _administrationService.GetUserProfiles();

            return users;
        }
    }
}
