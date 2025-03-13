using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AgileTeamsContext : IdentityDbContext<ApplicationUser>, IAgileTeamsContext
    {
        public AgileTeamsContext(DbContextOptions<AgileTeamsContext> options) : base(options) { }
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Ticket>()
                .HasKey(t => t.TicketID);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.TicketStatus)
                .WithMany()
                .HasForeignKey(t => t.TicketStatusID);

            modelBuilder.Entity<TicketStatus>()
                .HasKey(t => t.StatusID);

            modelBuilder.Entity<TicketType>()
                .HasKey(t => t.TypeID);

            modelBuilder.Entity<WorkItem>()
                .HasKey(t => t.WorkItemID);

            modelBuilder.Entity<WorkItemStatus>()
                .HasKey(t => t.StatusID);

            modelBuilder.Entity<WorkItemType>()
                .HasKey(t => t.TypeID);

            modelBuilder.Entity<WorkItemPriority>()
                .HasKey(t => t.PriorityID);

            modelBuilder.Entity<WorkItemComment>()
                .HasKey(t => t.CommentID);

            modelBuilder.Entity<Project>()
                .HasKey(t => t.ProjectID);
        }
    }
}
