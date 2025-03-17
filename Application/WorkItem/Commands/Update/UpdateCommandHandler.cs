using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Commands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Unit>
    {
        private readonly IWorkItemService _workItemService;
        public UpdateCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            await _workItemService.Update(request.WorkItem);

            return Unit.Value;
        }
    }
}
