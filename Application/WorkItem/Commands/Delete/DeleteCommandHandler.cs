using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Commands.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Unit>
    {
        private readonly IWorkItemService _workItemService;
        public DeleteCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await _workItemService.Delete(request.WorkItemId);

            return Unit.Value;
        }
    }
}
