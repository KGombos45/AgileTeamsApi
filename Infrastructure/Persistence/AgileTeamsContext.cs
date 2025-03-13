using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AgileTeamsContext : DbContext, IAgileTeamsContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkItemStatus> WorkItemStatuses { get; set; }
        public DbSet<WorkItemPriority> WorkItemPriorities { get; set; }
        public DbSet<WorkItemComment> WorkItemComments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<WorkItemType> WorkItemTypes { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        public AgileTeamsContext(DbContextOptions<AgileTeamsContext> options) : base(options) { }
    }
}
