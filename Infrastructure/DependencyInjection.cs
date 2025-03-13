using Application;
using Application.Common.Interfaces;
using AutoMapper;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AgileTeamsContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AgileTeamsConnection")));

        services.AddScoped<IAgileTeamsContext, AgileTeamsContext>();
        services.AddScoped<IAdministrationService, AdministrationService>();

        return services;
    }
}