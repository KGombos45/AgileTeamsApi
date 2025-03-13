using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Administration.Queries.GetUserProfiles;
using Domain.Entities.AgileTeams;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Administration.Commands.GetUserProfiles
{
    public class GetUserProfilesLoggingBehavior : IPipelineBehavior<GetUserProfilesQuery, List<ApplicationUser>>
    {
        private readonly ILogger<GetUserProfilesLoggingBehavior> _logger;

        public GetUserProfilesLoggingBehavior(ILogger<GetUserProfilesLoggingBehavior> logger)
        {
            _logger = logger;
        }

        public async Task<List<ApplicationUser>> Handle(GetUserProfilesQuery request, RequestHandlerDelegate<List<ApplicationUser>> next, CancellationToken cancellationToken)
        {
            var scope = request;

            using (_logger.BeginScope(scope))
            {
                return await next();
            }
        }
    }
}
