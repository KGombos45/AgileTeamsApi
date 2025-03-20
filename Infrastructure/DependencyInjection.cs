using System;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.AgileTeams;
using Infrastructure.Mappings;
using Infrastructure.Persistence;
using Infrastructure.Seeders;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            services.AddScoped<RoleSeeder>();

            services.AddSingleton<ApplicationSettings>();

            services.AddAutoMapper(typeof(ApplicationUserProfile));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AgileTeamsContext>()
                .AddDefaultTokenProviders();

            AddAppAuthorization(services, configuration);
            SeedInitialData(services);

            return services;
        }

        public static void SeedInitialData(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AgileTeamsContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
                AgileTeamsContext.SeedAsync(context, userManager, roleManager, roleSeeder).Wait();
            }
        }
        public static IServiceCollection AddAppAuthorization(IServiceCollection services, IConfiguration configuration)
        {
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
            })
             .AddJwtBearer(cfg =>
             {
                 cfg.RequireHttpsMetadata = true;
                 cfg.SaveToken = true;
                 cfg.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(token),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ClockSkew = TimeSpan.Zero
                 };
                 cfg.Events = new JwtBearerEvents
                 {
                     OnAuthenticationFailed = context =>
                     {
                         var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<AccountService>>();
                         logger.LogError("Authentication failed: " + context.Exception.Message);
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = context =>
                     {
                         var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<AccountService>>();
                         logger.LogInformation("Token validated successfully");
                         return Task.CompletedTask;
                     }
                 };
             });

            return services;
        }
    }
}

