
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
        private readonly IMapper _mapper;

        public TicketService(AgileTeamsContext context, 
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task CreateTicket(Ticket ticket)
        {
            try
            {
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException();
            }

        }
        public async Task UpdateTicket(Ticket ticket)
        {
            try
            {
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException();
            }
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
    }
}
