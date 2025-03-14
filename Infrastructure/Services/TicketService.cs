
using Application.Common.Interfaces;
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

        public TicketService(AgileTeamsContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public async Task<List<Array>> GetTicketStatusCount()
        {
            var counts = await _context.Tickets.Select(x => x.TicketStatus).GroupBy(i => i.StatusName).ToDictionaryAsync(g => g.Key, g => g.Count());

            var list = new List<Array>();

            foreach (var count in counts)
            {
                object[] countString = new object[] { count.Key, count.Value };

                list.Add(countString.ToArray());
            }

            return list;
        }

        public async Task<List<Array>> GetTicketTypeCount()
        {
            var counts = await _context.Tickets.Select(x => x.TicketType).GroupBy(i => i.TypeName).ToDictionaryAsync(g => g.Key, g => g.Count());

            var list = new List<Array>();

            foreach (var count in counts)
            {
                object[] countString = new object[] { count.Key, count.Value };

                list.Add(countString.ToArray());
            }

            return list;
        }
        public async Task<List<Array>> GetTicketOwnerCount()
        {
            var counts = await _context.Tickets.Select(x => x.TicketOwner).GroupBy(i => i.UserName).ToDictionaryAsync(g => g.Key, g => g.Count());

            var list = new List<Array>();

            foreach (var count in counts)
            {
                object[] countString = new object[] { count.Key, count.Value };

                list.Add(countString.ToArray());
            }

            return list;
        }
        public async Task<List<Ticket>> GetTickets()
        {
            var tickets = await _context.Tickets.Include(t => t.TicketOwner)
                .Include(t => t.TicketWorkItem)
                .Include(t => t.TicketStatus)
                .Include(t => t.TicketType).ToListAsync();

            return tickets;
        }
        public async Task<List<Ticket>> GetUserTickets(string userId)
        {
            var tickets = await _context.Tickets.Include(t => t.TicketOwner)
                                            .Include(t => t.TicketWorkItem)
                                            .Include(t => t.TicketStatus)
                                            .Include(t => t.TicketType)
                                            .Where(t => t.TicketOwnerID.Equals(userId)).ToListAsync();

            return tickets;
        }
    }
}
