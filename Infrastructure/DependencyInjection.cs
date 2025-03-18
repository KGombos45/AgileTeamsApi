using System;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.AgileTeams;
using Infrastructure.Mappings;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AgileTeamsContext>(options =>
                options.UseInMemoryDatabase("AgileTeamsConnection"));

            services.AddScoped<IAgileTeamsContext, AgileTeamsContext>();
            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IWorkItemService, WorkItemService>();
            services.AddSingleton<ApplicationSettings>();

            // Add Identity services
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AgileTeamsContext>()
                .AddDefaultTokenProviders();

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AgileTeamsContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                AgileTeamsContext.SeedAsync(context, userManager, roleManager).Wait();
            }

            // Add JWT Authentication
            // Bind ApplicationSettings section to ApplicationSettings class
            services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));

            // Add JWT Authentication
            var applicationSettings = configuration.GetSection("ApplicationSettings").Get<ApplicationSettings>();
            var token = Encoding.UTF8.GetBytes(applicationSettings.JWT_Token);

            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = false;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(token),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}