
using System.Net.Sockets;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities.AgileTeams;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AgileTeamsContext _context;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public ProjectService(AgileTeamsContext context,
            IAccountService accountService,
            IMapper mapper)
        {
            _context = context;
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task CreateProject(CreateProjectDto project)
        {
            var creator = await _accountService.GetLoggedInUser();

            if (creator == null)
            {
                throw new DirectoryNotFoundException();
            }

            var dbProject = new Project
            {
                CreatedBy = creator.UserName,
                CreatedOn = DateTime.Now
            };

            _mapper.Map(project, dbProject);

            await _context.Projects.AddAsync(dbProject);
            await _context.SaveChangesAsync();

            return;
        }

        public async Task UpdateProject(UpdateProjectDto project)
        {
            var existingProject = await _context.Projects.FindAsync(project.ProjectID);

            if (existingProject == null || project == null)
            {
                throw new DirectoryNotFoundException();
            }

            _mapper.Map(project, existingProject);

            _context.Projects.Update(existingProject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProject(string projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                throw new DirectoryNotFoundException();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task<List<ProjectDto>> GetProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.WorkItems).ThenInclude(w => w.Tickets)
                .Include(p => p.WorkItems).ThenInclude(w => w.WorkItemStatus)
                .Include(p => p.WorkItems).ThenInclude(w => w.WorkItemOwner)
                .Include(p => p.WorkItems).ThenInclude(w => w.Comments)
                .Include(p => p.WorkItems).ThenInclude(w => w.WorkItemType)
                .Include(p => p.WorkItems).ThenInclude(w => w.WorkItemPriority)
                .ToListAsync();

            return _mapper.Map<List<ProjectDto>>(projects);
        }
    }
}
