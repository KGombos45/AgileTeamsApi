using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Administration.Queries.GetRoles;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Administration.Commands.GetUserProfiles
{
    public class GetRolesLoggingBehavior : IPipelineBehavior<GetRolesQuery, List<string>>
    {
        private readonly ILogger<GetRolesLoggingBehavior> _logger;

        public GetRolesLoggingBehavior(ILogger<GetRolesLoggingBehavior> logger)
        {
            _logger = logger;
        }

        public async Task<List<string>> Handle(GetRolesQuery request, RequestHandlerDelegate<List<string>> next, CancellationToken cancellationToken)
        {
            var scope = request;

            using (_logger.BeginScope(scope))
            {
                return await next();
            }
        }
    }
}
