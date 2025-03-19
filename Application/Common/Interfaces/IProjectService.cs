namespace Application.Common.Interfaces
{
    using Application.Common.Models;
    using Project = Domain.Entities.AgileTeams.Project;
    public interface IProjectService
    {
        public Task CreateProject(Project project);
        public Task UpdateProject(UpdateProjectDto project);
        public Task DeleteProject(string projectId);
        public Task<List<ProjectDto>> GetProjects();
    }
}
