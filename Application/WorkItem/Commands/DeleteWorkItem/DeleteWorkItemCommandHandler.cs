using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Commands.DeleteWorkItem
{
    public class DeleteWorkItemCommandHandler : IRequestHandler<DeleteWorkItemCommand, Unit>
    {
        private readonly IWorkItemService _workItemService;
        public DeleteWorkItemCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<Unit> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            await _workItemService.Delete(request.WorkItemId);

            return Unit.Value;
        }
    }
}
