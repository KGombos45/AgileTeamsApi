using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Administration.Commands.DeleteUser
{
    public class DeleteUserLoggingBehavior : IPipelineBehavior<DeleteUserCommand, Unit> 
    {
        private readonly ILogger<DeleteUserLoggingBehavior> _logger;

        public DeleteUserLoggingBehavior(ILogger<DeleteUserLoggingBehavior> logger)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
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
