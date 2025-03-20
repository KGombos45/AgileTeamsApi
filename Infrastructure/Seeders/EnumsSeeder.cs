using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders
{
    public class EnumsSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AgileTeamsContext _context;
        public EnumsSeeder(RoleManager<IdentityRole> roleManager,
            AgileTeamsContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        public async Task SeedEnumsAsync()
        {
            await SeedRolesAsync();
            await SeedPriorities();
            await SeedTicketStatuses();
            await SeedTicketTypes();
            await SeedWorkItemStatuses();
            await SeedWorkItemTypes();
        }
        private async Task SeedRolesAsync()
        {
            foreach (var item in Enum.GetValues(typeof(IdentityRoles)).Cast<IdentityRoles>())
            {
                var role = item.GetDescription();

                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        private async Task SeedPriorities()
        {
            foreach (var item in Enum.GetValues(typeof(Priorities)).Cast<Priorities>())
            {
                var priority = new WorkItemPriority
                {
                    PriorityID = item,
                    PriorityName = item.GetDescription()
                };

                if (!await _context.WorkItemPriorities.ContainsAsync(priority))
                {
                    await _context.WorkItemPriorities.AddRangeAsync(priority);
                }
            }
        }
        private async Task SeedTicketStatuses()
        {
            foreach (var item in Enum.GetValues(typeof(TicketStatuses)).Cast<TicketStatuses>())
            {
                var ticket = new TicketStatus
                {
                    StatusID = item,
                    StatusName = item.GetDescription()
                };

                if (!await _context.TicketStatuses.ContainsAsync(ticket))
                {
                    await _context.TicketStatuses.AddRangeAsync(ticket);
                }
            }
        }
        private async Task SeedTicketTypes()
        {
            foreach (var item in Enum.GetValues(typeof(TicketTypes)).Cast<TicketTypes>())
            {
                var type = new TicketType
                {
                    TypeID = item,
                    TypeName = item.GetDescription()
                };

                if (!await _context.TicketTypes.ContainsAsync(type))
                {
                    await _context.TicketTypes.AddRangeAsync(type);
                }
            }
        }
        private async Task SeedWorkItemStatuses()
        {
            foreach (var item in Enum.GetValues(typeof(WorkItemStatuses)).Cast<WorkItemStatuses>())
            {
                var status = new WorkItemStatus
                {
                    StatusID = item,
                    StatusName = item.GetDescription()
                };

                if (!await _context.WorkItemStatuses.ContainsAsync(status))
                {
                    await _context.WorkItemStatuses.AddRangeAsync(status);
                }
            }
        }
        private async Task SeedWorkItemTypes()
        {
            foreach (var item in Enum.GetValues(typeof(WorkItemTypes)).Cast<WorkItemTypes>())
            {
                var type = new WorkItemType
                {
                    TypeID = item,
                    TypeName = item.GetDescription()
                };

                if (!await _context.WorkItemTypes.ContainsAsync(type))
                {
                    await _context.WorkItemTypes.AddRangeAsync(type);
                }
            }
        }
    }
}
