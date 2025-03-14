namespace Application.Common.Interfaces
{
    using Project = Domain.Entities.AgileTeams.Project;
    public interface IProjectService
    {
        public Task CreateProject(Project project);
        public Task UpdateProject(Project project);
        public Task DeleteProject(string projectId);
        public Task<List<Project>> GetProjects();
    }
}
