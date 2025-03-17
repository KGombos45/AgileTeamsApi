
using MediatR;

namespace Application.Project.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest<Unit>
    {
        public string ProjectId { get; set; }
    }
}
