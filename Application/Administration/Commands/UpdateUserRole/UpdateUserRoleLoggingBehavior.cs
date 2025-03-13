using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Administration.Commands.UpdateUserRole;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Administration.Commands.UpdateUserRole
{
    public class UpdateUserRoleLoggingBehavior : IPipelineBehavior<UpdateUserRoleCommand, Unit>
    {
        private readonly ILogger<UpdateUserRoleLoggingBehavior> _logger;

        public UpdateUserRoleLoggingBehavior(ILogger<UpdateUserRoleLoggingBehavior> logger)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateUserRoleCommand request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
        {
            var scope = new Dictionary<string, object>
            {
                {
                    request.UserId, request.User
                }
            };

            using (_logger.BeginScope(scope))
            {
                return await next();
            }
        }
    }
}
