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
        public Task<List<TicketDto>> GetTickets();
        public Task CreateTicket(CreateTicketDto ticket);
        public Task UpdateTicket(UpdateTicketDto ticket);
        public Task DeleteTicket(string id);
        public Task<List<TicketDto>> GetUserTickets(string userId);
        public Task<List<TicketStatus>> GetStatuses();
        public Task<List<TicketType>> GetTypes();
        public Task<List<CountResponse>> GetTicketStatusCount();
        public Task<List<CountResponse>> GetTicketTypeCount();
        public Task<List<CountResponse>> GetTicketOwnerCount();
    }
}
