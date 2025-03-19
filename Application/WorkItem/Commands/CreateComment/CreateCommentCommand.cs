using Application.Common.Models;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.WorkItem.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<Unit>
    {
        public CommentRequest Comment { get; set; }
    }
}
