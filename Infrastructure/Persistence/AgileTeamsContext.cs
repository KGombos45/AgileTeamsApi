using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using Domain.Enums;
using Infrastructure.Seeders;
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

            modelBuilder.Entity<Project>()
                .HasKey(t => t.ProjectID);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.WorkItems)
                .WithOne(w => w.Project)
                .HasForeignKey(w => w.WorkItemProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkItem>(entity =>
            {
                entity.HasKey(t => t.WorkItemID);
                entity.HasOne(w => w.WorkItemType)
                    .WithMany()
                    .HasForeignKey(w => w.WorkItemTypeID);
                entity.HasOne(w => w.WorkItemPriority)
                    .WithMany()
                    .HasForeignKey(w => w.WorkItemPriorityID);
                entity.HasOne(w => w.WorkItemOwner)
                    .WithMany()
                    .HasForeignKey(w => w.WorkItemOwnerID);
                entity.HasMany(w => w.Tickets)
                    .WithOne(t => t.TicketWorkItem)
                    .HasForeignKey(t => t.TicketWorkItemID)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(w => w.Comments)
                    .WithOne(c => c.WorkItem)
                    .HasForeignKey(c => c.CommentWorkItemID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(t => t.TicketID);
                entity.HasOne(t => t.TicketStatus)
                    .WithMany()
                    .HasForeignKey(t => t.TicketStatusID);
                entity.HasOne(t => t.TicketType)
                    .WithMany()
                    .HasForeignKey(t => t.TicketTypeID);
                entity.HasOne(t => t.TicketOwner)
                    .WithMany()
                    .HasForeignKey(t => t.TicketOwnerID);
            });

            modelBuilder.Entity<WorkItemStatus>()
                .HasKey(t => t.StatusID);

            modelBuilder.Entity<WorkItemPriority>()
                .HasKey(t => t.PriorityID);

            modelBuilder.Entity<WorkItemType>()
                .HasKey(t => t.TypeID);

            modelBuilder.Entity<WorkItemComment>(entity =>
            {
                entity.HasKey(t => t.CommentID);
                entity.Property(t => t.CommentID)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TicketStatus>()
                .HasKey(t => t.StatusID);

            modelBuilder.Entity<TicketType>()
                .HasKey(t => t.TypeID);
        }

        public static async Task SeedAsync(AgileTeamsContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, RoleSeeder roleSeeder)
        {
            if (!context.Roles.Any())
            {
                await roleSeeder.SeedRolesAsync();
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
                    Role = IdentityRoles.Admin.GetDescription(),
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
                    Role = IdentityRoles.Developer.GetDescription(),
                };
                await userManager.CreateAsync(user2, "Password123!");
                await userManager.AddToRoleAsync(user2, user2.Role);
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

            await context.SaveChangesAsync();

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
                await context.SaveChangesAsync();
            }

            if (!context.WorkItems.Any())
            {
                var project1 = context.Projects.First(p => p.ProjectID == "1");
                var project2 = context.Projects.First(p => p.ProjectID == "2");
                var user1 = context.Users.First(u => u.UserName == "user1");
                var user2 = context.Users.First(u => u.UserName == "user2");    

                var workItem1 = new WorkItem
                {
                    WorkItemID = "1",
                    WorkItemName = "WorkItem 1",
                    WorkItemDescription = "Description for WorkItem 1",
                    WorkItemProjectID = "1",
                    WorkItemStatusID = 1,
                    WorkItemTypeID = 1,
                    WorkItemPriorityID = 1,
                    WorkItemOwnerID = user1.Id,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = "user1",
                    ModifiedBy = "user1",
                    Project = project1,
                    Tickets = new List<Ticket>(),
                    Comments = new List<WorkItemComment>()
                };

                var workItem2 = new WorkItem
                {
                    WorkItemID = "2",
                    WorkItemName = "WorkItem 2",
                    WorkItemDescription = "Description for WorkItem 2",
                    WorkItemProjectID = "2",
                    WorkItemStatusID = 2,
                    WorkItemTypeID = 2,
                    WorkItemPriorityID = 2,
                    WorkItemOwnerID = user2.Id,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = "user2",
                    ModifiedBy = "user2",
                    Project = project2,
                    Tickets = new List<Ticket>(),
                    Comments = new List<WorkItemComment>()
                };

                context.WorkItems.AddRange(workItem1, workItem2);
                await context.SaveChangesAsync();
            }

            if (!context.Tickets.Any())
            {
                var workItem1 = context.WorkItems.First(w => w.WorkItemID == "1");
                var workItem2 = context.WorkItems.First(w => w.WorkItemID == "2");
                var user1 = context.Users.First(u => u.UserName == "user1");
                var user2 = context.Users.First(u => u.UserName == "user2");

                context.Tickets.AddRange(
                    new Ticket
                    {
                        TicketID = "1",
                        TicketName = "Ticket 1",
                        TicketDescription = "Description for Ticket 1",
                        CreatedBy = "user1",
                        TicketStatusID = 1,
                        TicketTypeID = 1,
                        TicketOwnerID = user1.Id,
                        TicketWorkItemID = "1",
                        CreatedOn = DateTime.UtcNow,
                        ModifiedBy = "user1",
                        TicketWorkItem = workItem1
                    },
                    new Ticket
                    {
                        TicketID = "2",
                        TicketName = "Ticket 2",
                        TicketDescription = "Description for Ticket 2",
                        CreatedBy = "user2",
                        TicketStatusID = 2,
                        TicketTypeID = 2,
                        TicketOwnerID = user2.Id,
                        TicketWorkItemID = "2",
                        CreatedOn = DateTime.UtcNow,
                        ModifiedBy = "user2",
                        TicketWorkItem = workItem2
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.WorkItemComments.Any())
            {
                var workItem1 = context.WorkItems.First(w => w.WorkItemID == "1");
                var workItem2 = context.WorkItems.First(w => w.WorkItemID == "2");

                context.WorkItemComments.AddRange(
                    new WorkItemComment
                    {
                        CommentID = "1",
                        Comment = "Comment for WorkItem 1",
                        SubmittedBy = "user1",
                        SubmittedOn = DateTime.UtcNow,
                        CommentWorkItemID = "1",
                        WorkItem = workItem1
                    },
                    new WorkItemComment
                    {
                        CommentID = "2",
                        Comment = "Comment for WorkItem 2",
                        SubmittedBy = "user2",
                        SubmittedOn = DateTime.UtcNow,
                        CommentWorkItemID = "2",
                        WorkItem = workItem2
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}