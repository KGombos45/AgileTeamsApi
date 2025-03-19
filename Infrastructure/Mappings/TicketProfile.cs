using Application.Common.Models;
using AutoMapper;
using Domain.Entities.AgileTeams;

namespace Infrastructure.Mappings
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketDto, Ticket>();
            CreateMap<Ticket, TicketDto>();
            CreateMap<WorkItem, TicketWorkItemDto>();
        }
    }
}
