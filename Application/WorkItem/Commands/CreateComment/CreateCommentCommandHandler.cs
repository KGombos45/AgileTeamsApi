using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkItem.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
    {
        private readonly IWorkItemService _workItemService;
        public CreateCommentCommandHandler(IWorkItemService workItemService)
        {
            _workItemService = workItemService;
        }
        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            await _workItemService.CreateComment(request.WorkItemComment);

            return Unit.Value;
        }
    }
}
