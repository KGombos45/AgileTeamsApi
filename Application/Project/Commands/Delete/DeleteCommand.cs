
using MediatR;

namespace Application.Project.Commands.Delete
{
    public class DeleteCommand : IRequest<Unit>
    {
        public string ProjectId { get; set; }
    }
}
