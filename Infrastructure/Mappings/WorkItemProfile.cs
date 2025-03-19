
using Application.Common.Models;
using AutoMapper;
using Domain.Entities.AgileTeams;

namespace Infrastructure.Mappings
{
    public class WorkItemProfile : Profile
    {
        public WorkItemProfile()
        {
            CreateMap<WorkItemDto, WorkItem>();
            CreateMap<WorkItem, WorkItemDto>();
            CreateMap<Ticket, WorkItemTicketDto>();
            CreateMap<Project, WorkItemProjectDto>();
            CreateMap<WorkItemComment, WorkItemCommentDto>();

            CreateMap<UpdateWorkItemDto, WorkItem>()
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}