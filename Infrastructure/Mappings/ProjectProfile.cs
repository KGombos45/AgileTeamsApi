using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities.AgileTeams;

namespace Infrastructure.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDto, Project>();
            CreateMap<Project, ProjectDto>();
            CreateMap<WorkItem, ProjectWorkItemDto>();
        }
    }
}
