using Application.Common.Models;
using AutoMapper;
using Domain.Entities.AgileTeams;

namespace Infrastructure.Mappings
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<ApplicationUserDto, ApplicationUser>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<RegistrationRequest, ApplicationUser>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"));
        }
    }
}
