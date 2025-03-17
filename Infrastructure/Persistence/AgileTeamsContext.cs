using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project = Domain.Entities.AgileTeams.Project;

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

        public static async Task SeedAsync(AgileTeamsContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!context.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!context.Users.Any())
            {
                var user1 = new ApplicationUser
                {
                    UserName = "user1",
                    Email = "user1@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    EmailConfirmed = true,
                    Role = "Admin"
                };
                await userManager.CreateAsync(user1, "Password123!");
                await userManager.AddToRoleAsync(user1, user1.Role);

                var user2 = new ApplicationUser
                {
                    UserName = "user2",
                    Email = "user2@example.com",
                    FirstName = "Jane",
                    LastName = "Doe",
                    EmailConfirmed = true,
                    Role = "User",
                };
                await userManager.CreateAsync(user2, "Password123!");
                await userManager.AddToRoleAsync(user2, user2.Role);
            }

            if (!context.Projects.Any())
            {
                context.Projects.AddRange(
                    new Project
                    {
                        ProjectID = "1",
                        ProjectName = "Project 1",
                        Description = "Description for Project 1",
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = "user1",
                        WorkItems = new List<WorkItem>()
                    },
                    new Project
                    {
                        ProjectID = "2",
                        ProjectName = "Project 2",
                        Description = "Description for Project 2",
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = "user2",
                        WorkItems = new List<WorkItem>()
                    }
                );
            }

            if (!context.WorkItemStatuses.Any())
            {
                context.WorkItemStatuses.AddRange(
                    new WorkItemStatus { StatusID = 1, StatusName = "Open" },
                    new WorkItemStatus { StatusID = 2, StatusName = "In Progress" },
                    new WorkItemStatus { StatusID = 3, StatusName = "Closed" }
                );
            }

            if (!context.WorkItemPriorities.Any())
            {
                context.WorkItemPriorities.AddRange(
                    new WorkItemPriority { PriorityID = 1, PriorityName = "Low" },
                    new WorkItemPriority { PriorityID = 2, PriorityName = "Medium" },
                    new WorkItemPriority { PriorityID = 3, PriorityName = "High" }
                );
            }

            if (!context.WorkItemTypes.Any())
            {
                context.WorkItemTypes.AddRange(
                    new WorkItemType { TypeID = 1, TypeName = "Bug" },
                    new WorkItemType { TypeID = 2, TypeName = "Feature" },
                    new WorkItemType { TypeID = 3, TypeName = "Task" }
                );
            }

            if (!context.TicketStatuses.Any())
            {
                context.TicketStatuses.AddRange(
                    new TicketStatus { StatusID = 1, StatusName = "New" },
                    new TicketStatus { StatusID = 2, StatusName = "In Progress" },
                    new TicketStatus { StatusID = 3, StatusName = "Resolved" }
                );
            }

            if (!context.TicketTypes.Any())
            {
                context.TicketTypes.AddRange(
                    new TicketType { TypeID = 1, TypeName = "Bug" },
                    new TicketType { TypeID = 2, TypeName = "Feature" },
                    new TicketType { TypeID = 3, TypeName = "Task" }
                );
            }

            if (!context.WorkItems.Any())
            {
                context.WorkItems.AddRange(
                    new WorkItem
                    {
                        WorkItemID = "1",
                        WorkItemName = "WorkItem 1",
                        WorkItemDescription = "Description for WorkItem 1",
                        WorkItemProjectID = "1",
                        WorkItemStatusID = 1,
                        WorkItemTypeID = 1,
                        WorkItemPriorityID = 1,
                        WorkItemOwnerID = "1",
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = "user1",
                        ModifiedBy = "user1",
                        Project = context.Projects.Find("1"),
                    },
                    new WorkItem
                    {
                        WorkItemID = "2",
                        WorkItemName = "WorkItem 2",
                        WorkItemDescription = "Description for WorkItem 2",
                        WorkItemProjectID = "2",
                        WorkItemStatusID = 2,
                        WorkItemTypeID = 2,
                        WorkItemPriorityID = 2,
                        WorkItemOwnerID = "2",
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = "user2",
                        ModifiedBy = "user2",
                        Project = context.Projects.Find("2"),
                    }
                );
            }

            if (!context.Tickets.Any())
            {
                context.Tickets.AddRange(
                    new Ticket
                    {
                        TicketID = "1",
                        TicketName = "Ticket 1",
                        TicketDescription = "Description for Ticket 1",
                        CreatedBy = "user1",
                        TicketStatusID = 1,
                        TicketTypeID = 1,
                        TicketOwnerID = "1",
                        TicketWorkItemID = "1",
                        CreatedOn = DateTime.UtcNow,
                        ModifiedBy = "user1"
                    },
                    new Ticket
                    {
                        TicketID = "2",
                        TicketName = "Ticket 2",
                        TicketDescription = "Description for Ticket 2",
                        CreatedBy = "user2",
                        TicketStatusID = 2,
                        TicketTypeID = 2,
                        TicketOwnerID = "2",
                        TicketWorkItemID = "2",
                        CreatedOn = DateTime.UtcNow,
                        ModifiedBy = "user2"
                    }
                );
            }

            if (!context.WorkItemComments.Any())
            {
                context.WorkItemComments.AddRange(
                    new WorkItemComment
                    {
                        CommentID = "1",
                        Comment = "Comment for WorkItem 1",
                        SubmittedBy = "user1",
                        SubmittedOn = DateTime.UtcNow,
                        CommentWorkItemID = "1"
                    },
                    new WorkItemComment
                    {
                        CommentID = "2",
                        Comment = "Comment for WorkItem 2",
                        SubmittedBy = "user2",
                        SubmittedOn = DateTime.UtcNow,
                        CommentWorkItemID = "2"
                    }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}
