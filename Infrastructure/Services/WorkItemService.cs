using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly AgileTeamsContext _context;
        private UserManager<ApplicationUser> _userManager;

        public WorkItemService(AgileTeamsContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task Create(WorkItem workItem)
        {
            await _context.WorkItems.AddAsync(workItem);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task Update(WorkItem workItem)
        {
            _context.WorkItems.Update(workItem);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task Delete(string workItemId)
        {
            var workItem = await _context.WorkItems.FindAsync(workItemId);

            if (workItem == null)
            {
                throw new DirectoryNotFoundException();
            }

            _context.WorkItemComments.RemoveRange(workItem.Comments);
            _context.Tickets.RemoveRange(workItem.Tickets);
            _context.WorkItems.Remove(workItem);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task CreateComment(WorkItemComment workItemComment)
        {
            await _context.WorkItemComments.AddAsync(workItemComment);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task<List<WorkItemStatus>> GetStatuses()
        {
            var statuses = await _context.WorkItemStatuses.ToListAsync();

            return statuses;
        }
        public async Task<List<WorkItemType>> GetTypes()
        {
            var types = await _context.WorkItemTypes.ToListAsync();

            return types;
        }
        public async Task<List<WorkItemPriority>> GetPriorities()
        {
            var priorities = await _context.WorkItemPriorities.ToListAsync();

            return priorities;
        }
        public async Task<List<WorkItem>> GetWorkItems()
        {
            var workItems = await _context.WorkItems
                .Include(w => w.Project)
                .Include(w => w.WorkItemStatus)
                .Include(w => w.WorkItemType)
                .Include(w => w.WorkItemOwner)
                .Include(w => w.WorkItemPriority)
                .Include(w => w.Comments)
                .Include(w => w.Tickets).ThenInclude(t => t.TicketOwner)
                .Include(w => w.Tickets).ThenInclude(t => t.TicketStatus).ToListAsync();

            return workItems;
        }
        public async Task<List<Array>> GetWorkItemStatusCount() {

            var counts = await _context.WorkItems.Select(x => x.WorkItemStatus).GroupBy(i => i.StatusName).ToDictionaryAsync(g => g.Key, g => g.Count());
            var list = new List<Array>();

            foreach (var count in counts)
            {
                object[] countString = new object[] { count.Key, count.Value };

                list.Add(countString.ToArray());
            }

            return list;
        }
        public async Task<List<Array>> GetWorkItemPriorityCount()
        {
            var counts = await _context.WorkItems.Select(x => x.WorkItemPriority).GroupBy(i => i.PriorityName).ToDictionaryAsync(g => g.Key, g => g.Count());
            var list = new List<Array>();

            foreach (var count in counts)
            {
                object[] countString = new object[] { count.Key, count.Value };

                list.Add(countString.ToArray());
            }

            return list;
        }
        public async Task<List<Array>> GetWorkItemOwnerCount()
        {
            var counts = await _context.WorkItems.Select(x => x.WorkItemOwner).GroupBy(i => i.UserName).ToDictionaryAsync(g => g.Key, g => g.Count());
            var list = new List<Array>();

            foreach (var count in counts)
            {
                object[] countString = new object[] { count.Key, count.Value };

                list.Add(countString.ToArray());
            }

            return list;
        }
        public async Task<List<WorkItem>> GetUserWorkItems(string userId)
        {
            var workItems = await _context.WorkItems
                .Include(w => w.Project)
                .Include(w => w.WorkItemStatus)
                .Include(w => w.WorkItemType)
                .Include(w => w.WorkItemOwner)
                .Include(w => w.WorkItemPriority)
                .Include(w => w.Comments)
                .Include(w => w.Tickets).ThenInclude(t => t.TicketOwner)
                .Include(w => w.Tickets).ThenInclude(t => t.TicketStatus)
                .Where(w => w.WorkItemOwnerID.Equals(userId)).ToListAsync();

            return workItems;
        }
    }
}
