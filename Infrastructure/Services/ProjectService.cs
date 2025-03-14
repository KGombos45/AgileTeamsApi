
using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AgileTeamsContext _context;

        public ProjectService(AgileTeamsContext context)
        {
            _context = context;
        }
        public async Task CreateProject(Project project)
        {
            try
            {
                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException();
            }

            return;
        }

        public async Task UpdateProject(Project project)
        {
            try
            {
                _context.Projects.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException();
            }

            return;
        }

        public async Task DeleteProject(string projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                throw new DirectoryNotFoundException();
            }

            if (project.WorkItems.Any())
            {
                foreach (var workItem in project.WorkItems)
                {
                    _context.Tickets.RemoveRange(workItem.Tickets);
                }

                _context.WorkItems.RemoveRange(project.WorkItems);
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task<List<Project>> GetProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.WorkItems).ThenInclude(w => w.Tickets)
                .Include(p => p.WorkItems).ThenInclude(w => w.WorkItemStatus)
                .Include(p => p.WorkItems).ThenInclude(w => w.WorkItemOwner)
                .Include(p => p.WorkItems).ThenInclude(w => w.Comments)
                .Include(p => p.WorkItems).ThenInclude(w => w.WorkItemType)
                .Include(p => p.WorkItems).ThenInclude(w => w.Project)
                .Include(p => p.WorkItems).ThenInclude(w => w.WorkItemPriority).ToListAsync();

            return projects;
        }
    }
}
