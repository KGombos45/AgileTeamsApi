using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Mvc;

namespace Application.Common.Interfaces
{
    using Ticket = Domain.Entities.AgileTeams.Ticket;
    public interface ITicketService
    {
        public Task<List<Ticket>> GetTickets();
        public Task CreateTicket(Ticket ticket);
        public Task UpdateTicket(Ticket ticket);
        public Task DeleteTicket(string id);
        public Task<List<Ticket>> GetUserTickets(string userId);
        public Task<List<TicketStatus>> GetStatuses();
        public Task<List<TicketType>> GetTypes();
        public Task<List<Array>> GetTicketStatusCount();
        public Task<List<Array>> GetTicketTypeCount();
        public Task<List<Array>> GetTicketOwnerCount();
    }
}
