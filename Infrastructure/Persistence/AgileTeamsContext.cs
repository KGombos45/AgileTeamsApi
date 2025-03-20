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

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(t => t.Id);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(t => t.ProjectID);
                entity.Property(t => t.ProjectID)
                    .ValueGeneratedOnAdd();
                entity.HasMany(p => p.WorkItems)
                    .WithOne(w => w.Project)
                    .HasForeignKey(w => w.WorkItemProjectID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WorkItem>(entity =>
            {
                entity.HasKey(t => t.WorkItemID);
                entity.Property(t => t.WorkItemID)
                    .ValueGeneratedOnAdd();
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
                entity.Property(t => t.TicketID)
                    .ValueGeneratedOnAdd();
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

        public static async Task SeedAsync(AgileTeamsContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, EnumsSeeder enumsSeeder)
        {
            if (!context.Roles.Any())
            {
                await enumsSeeder.SeedEnumsAsync();
                await context.SaveChangesAsync();
            }

            var project1Id = "e0a7f5a7-44d8-4c98-9c18-5b87c0ff13ff";
            var project2Id = "4d89bb4d-53a4-4b1f-b073-22e0c5b8d446";
            var workItem1Id = "adf4b8fa-bb0f-44bc-9e0f-1ed3f0749c1b";
            var workItem2Id = "d3e81a2f-f7a4-464d-8906-e4e1f18a8a43";

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

            await context.SaveChangesAsync();

            if (!context.Projects.Any())
            {
                context.Projects.AddRange(
                    new Project
                    {
                        ProjectID = project1Id,
                        ProjectName = "Project 1",
                        Description = "Description for Project 1",
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = "user1",
                        WorkItems = new List<WorkItem>()
                    },
                    new Project
                    {
                        ProjectID = project2Id,
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
                var project1 = context.Projects.First(p => p.ProjectID == project1Id);
                var project2 = context.Projects.First(p => p.ProjectID == project2Id);
                var user1 = context.Users.First(u => u.UserName == "user1");
                var user2 = context.Users.First(u => u.UserName == "user2");    

                var workItem1 = new WorkItem
                {
                    WorkItemID = workItem1Id,
                    WorkItemName = "WorkItem 1",
                    WorkItemDescription = "Description for WorkItem 1",
                    WorkItemProjectID = project1.ProjectID,
                    WorkItemStatusID = Domain.Enums.WorkItemStatuses.Refining,
                    WorkItemTypeID = Domain.Enums.WorkItemTypes.Task,
                    WorkItemPriorityID = Priorities.Medium,
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
                    WorkItemID = workItem2Id,
                    WorkItemName = "WorkItem 2",
                    WorkItemDescription = "Description for WorkItem 2",
                    WorkItemProjectID = project2.ProjectID,
                    WorkItemStatusID = Domain.Enums.WorkItemStatuses.InProgress,
                    WorkItemTypeID = Domain.Enums.WorkItemTypes.Defect,
                    WorkItemPriorityID = Priorities.Low,
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
                var workItem1 = context.WorkItems.First(w => w.WorkItemID == workItem1Id);
                var workItem2 = context.WorkItems.First(w => w.WorkItemID == workItem2Id);
                var user1 = context.Users.First(u => u.UserName == "user1");
                var user2 = context.Users.First(u => u.UserName == "user2");

                context.Tickets.AddRange(
                    new Ticket
                    {
                        TicketName = "Ticket 1",
                        TicketDescription = "Description for Ticket 1",
                        CreatedBy = "user1",
                        TicketStatusID = Domain.Enums.TicketStatuses.InProgress,
                        TicketTypeID = Domain.Enums.TicketTypes.Task,
                        TicketOwnerID = user1.Id,
                        TicketWorkItemID = workItem1.WorkItemID,
                        CreatedOn = DateTime.UtcNow,
                        ModifiedBy = "user1",
                        TicketWorkItem = workItem1
                    },
                    new Ticket
                    {
                        TicketName = "Ticket 2",
                        TicketDescription = "Description for Ticket 2",
                        CreatedBy = "user2",
                        TicketStatusID = Domain.Enums.TicketStatuses.Defined,
                        TicketTypeID = Domain.Enums.TicketTypes.Bug,
                        TicketOwnerID = user2.Id,
                        TicketWorkItemID = workItem2.WorkItemID,
                        CreatedOn = DateTime.UtcNow,
                        ModifiedBy = "user2",
                        TicketWorkItem = workItem2
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.WorkItemComments.Any())
            {
                var workItem1 = context.WorkItems.First(w => w.WorkItemID == workItem1Id);
                var workItem2 = context.WorkItems.First(w => w.WorkItemID == workItem2Id);

                context.WorkItemComments.AddRange(
                    new WorkItemComment
                    {
                        Comment = "Comment for WorkItem 1",
                        SubmittedBy = "user1",
                        SubmittedOn = DateTime.UtcNow,
                        CommentWorkItemID = "1",
                        WorkItem = workItem1
                    },
                    new WorkItemComment
                    {
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