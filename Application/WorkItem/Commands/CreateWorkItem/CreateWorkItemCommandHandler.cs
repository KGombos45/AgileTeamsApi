using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Commands.CreateWorkItem
{
    public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, Unit>
    {
        private readonly IWorkItemService _workItemService;
        public CreateWorkItemCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<Unit> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            await _workItemService.Create(request.WorkItem);

            return Unit.Value;
        }
    }
}
