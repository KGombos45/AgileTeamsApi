using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Administration.Commands.DeleteUser;
using Application.Administration.Commands.GetUserProfiles;
using Application.Administration.Commands.UpdateUserRole;
using Application.Administration.Queries.GetRoles;
using Application.Administration.Queries.GetUserProfiles;
using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.TeamFoundation.TestManagement.WebApi;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UpdateUserRoleLoggingBehavior).Assembly));
            return services;
        }
    }
}
