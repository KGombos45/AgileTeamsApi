using Application.Common.Interfaces;
using MediatR;

namespace Application.Administration.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IAdministrationService _administrationService;

        public DeleteUserCommandHandler(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _administrationService.DeleteUser(request.UserId);

            return Unit.Value;
        }
    }
}
