using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Mappings
{
    public class ProjectMaps : Profile
    {
        public ProjectMaps()
        {
            CreateMap<Domain.Entities.AgileTeams.Project, ProjectWithWorkItems>();
        }
    }
}
