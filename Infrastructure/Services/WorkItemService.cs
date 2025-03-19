using System.Security.Claims;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities.AgileTeams;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Users;

namespace Infrastructure.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly AgileTeamsContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IAccountService _accountService;
        private IMapper _mapper;
        public WorkItemService(AgileTeamsContext context, 
            UserManager<ApplicationUser> userManager, 
            IAccountService accountService,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _accountService = accountService;
            _mapper = mapper;
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

            _context.WorkItems.Remove(workItem);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task CreateComment(CommentRequest comment)
        {
            var user = await _accountService.GetLoggedInUser();
            var workItem = await _context.WorkItems.FindAsync(comment.WorkItemId);

            if (user?.UserName == null)
            {
                throw new InvalidOperationException("User is not logged in or no associated username could be found.");
            }

            if (workItem == null)
            {
                throw new ArgumentException("Work item not found.", nameof(workItem));
            }

            var fullComment = new WorkItemComment
            {
                Comment = comment.Comment,
                CommentWorkItemID = comment.WorkItemId,
                SubmittedBy = user.UserName,
                SubmittedOn = DateTime.Now,
                WorkItem = workItem
            };

            await _context.WorkItemComments.AddAsync(fullComment);
            await _context.SaveChangesAsync();
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
        public async Task<List<WorkItemDto>> GetWorkItems()
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

            return _mapper.Map<List<WorkItemDto>>(workItems);
        }
        public async Task<List<CountResponse>> GetWorkItemStatusCount() {

            var workItems = await _context.WorkItems
                .Select(x => x.WorkItemStatus)
                .ToListAsync();

            var counts = workItems
                .GroupBy(i => i.StatusName)
                .Select(g => new CountResponse { Name = g.Key, Count = g.Count() })
                .ToList();

            return counts;
        }
        public async Task<List<CountResponse>> GetWorkItemPriorityCount()
        {
            var workItems = await _context.WorkItems
                .Select(x => x.WorkItemPriority)
                .ToListAsync();

            var counts = workItems
                .GroupBy(i => i.PriorityName)
                .Select(g => new CountResponse { Name = g.Key, Count = g.Count() })
                .ToList();

            return counts;
        }
        public async Task<List<CountResponse>> GetWorkItemOwnerCount()
        {
            var workItems = await _context.WorkItems
                .Select(x => x.WorkItemOwner)
                .ToListAsync();

            var counts = workItems
                .GroupBy(i => i.UserName)
                .Select(g => new CountResponse { Name = g.Key, Count = g.Count() })
                .ToList();

            return counts;
        }
        public async Task<List<WorkItemDto>> GetUserWorkItems(string userId)
        {
            var workItems = await _context.WorkItems.Where(w => w.WorkItemOwnerID.Equals(userId)).ToListAsync();

            return _mapper.Map<List<WorkItemDto>>(workItems);
        }
    }
}
