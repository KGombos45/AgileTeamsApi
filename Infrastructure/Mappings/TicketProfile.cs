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
            CreateMap<CreateTicketDto, Ticket>();
            CreateMap<Ticket, TicketDto>();
            CreateMap<WorkItem, TicketWorkItemDto>();
            CreateMap<UpdateTicketDto, Ticket>()
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
