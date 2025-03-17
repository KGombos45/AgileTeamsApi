using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.WorkItem.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<Unit>
    {
        public WorkItemComment WorkItemComment { get; set; }
    }
}
