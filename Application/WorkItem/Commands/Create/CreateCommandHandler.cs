using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Commands.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, Unit>
    {
        private readonly IWorkItemService _workItemService;
        public CreateCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            await _workItemService.Create(request.WorkItem);

            return Unit.Value;
        }
    }
}
