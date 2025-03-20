
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities.AgileTeams;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly AgileTeamsContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IAccountService _accountService;
        private readonly IMapper _mapper;

        public TicketService(AgileTeamsContext context, 
            UserManager<ApplicationUser> userManager,
            IAccountService accountService,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task CreateTicket(CreateTicketDto ticket)
        {
            var creator = await _accountService.GetLoggedInUser();

            if (creator == null)
            {
                throw new DirectoryNotFoundException();
            }

            var dbTicket = new Ticket
            {
                CreatedBy = creator.UserName,
                CreatedOn = DateTime.Now
            };

            _mapper.Map(ticket, dbTicket);

            await _context.Tickets.AddAsync(dbTicket);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task UpdateTicket(UpdateTicketDto ticket)
        {
            var existingTicket = await _context.Tickets.FindAsync(ticket.TicketID);

            var updateUser = await _accountService.GetLoggedInUser();

            if (ticket == null || existingTicket == null || updateUser == null)
            {
                throw new InvalidOperationException("Work item not found.");
            }

            existingTicket.ModifiedBy = updateUser.UserName;

            _mapper.Map(ticket, existingTicket);

            _context.Tickets.Update(existingTicket);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTicket(string id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                throw new DirectoryNotFoundException();
            }

            try
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException();
            }
        }
        public async Task<List<TicketDto>> GetTickets()
        {
            var tickets = await _context.Tickets.Include(t => t.TicketOwner)
                .Include(t => t.TicketWorkItem)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType).ToListAsync();

            return _mapper.Map<List<TicketDto>>(tickets);
        }
        public async Task<List<TicketDto>> GetUserTickets(string userId)
        {
            var tickets = await _context.Tickets.Include(t => t.TicketOwner)
                                            .Include(t => t.TicketWorkItem)
                                            .Include(t => t.TicketStatus)
                                            .Include(t => t.TicketType)
                                            .Where(t => t.TicketOwnerID.Equals(userId)).ToListAsync();

            return _mapper.Map<List<TicketDto>>(tickets);
        }
        public async Task<List<TicketStatus>> GetStatuses()
        {
            var statuses = await _context.TicketStatuses.ToListAsync();

            return statuses;

        }
        public async Task<List<TicketType>> GetTypes()
        {
            var types = await _context.TicketTypes.ToListAsync();

            return types;

        }
        public async Task<List<CountResponse>> GetTicketStatusCount()
        {
            var tickets = await _context.Tickets
                .Select(x => x.TicketStatus)
                .ToListAsync();

            var counts = tickets
                .GroupBy(i => i.StatusName)
                .Select(g => new CountResponse { Name = g.Key, Count = g.Count() })
                .ToList();

            return counts;
        }
        public async Task<List<CountResponse>> GetTicketTypeCount()
        {
            var tickets = await _context.Tickets
                .Select(x => x.TicketType)
                .ToListAsync();

            var counts = tickets
                .GroupBy(i => i.TypeName)
                .Select(g => new CountResponse { Name = g.Key, Count = g.Count() })
                .ToList();

            return counts;
        }
        public async Task<List<CountResponse>> GetTicketOwnerCount()
        {
            var tickets = await _context.Tickets
                .Select(x => x.TicketOwner)
                .ToListAsync();

            var counts = tickets
                .GroupBy(i => i.UserName)
                .Select(g => new CountResponse { Name = g.Key, Count = g.Count() })
                .ToList();

            return counts;
        }
    }
}
