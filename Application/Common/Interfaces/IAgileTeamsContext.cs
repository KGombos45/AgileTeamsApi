using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IAgileTeamsContext: IDbContext
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

    }
}
