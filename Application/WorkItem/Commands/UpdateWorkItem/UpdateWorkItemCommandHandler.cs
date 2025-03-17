using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Commands.UpdateWorkItem
{
    public class UpdateWorkItemCommandHandler : IRequestHandler<UpdateWorkItemCommand, Unit>
    {
        private readonly IWorkItemService _workItemService;
        public UpdateWorkItemCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<Unit> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            await _workItemService.Update(request.WorkItem);

            return Unit.Value;
        }
    }
}
