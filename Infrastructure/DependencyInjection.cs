using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.AgileTeams;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AgileTeamsContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AgileTeamsConnection"),
                b => b.MigrationsAssembly("Api")));

            services.AddScoped<IAgileTeamsContext, AgileTeamsContext>();
            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddSingleton<ApplicationUserAuth>();

            // Add Identity services
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AgileTeamsContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}