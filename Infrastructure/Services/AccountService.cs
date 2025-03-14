
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.AgileTeams;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Services.Users;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly AgileTeamsContext _agileTeamsContext;
        private readonly ApplicationUserAuth _applicationUserAuth;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AccountService(UserManager<ApplicationUser> userManager, AgileTeamsContext agileTeamsContext, ApplicationUserAuth applicationUserAuth, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _agileTeamsContext = agileTeamsContext;
            _applicationUserAuth = applicationUserAuth;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityResult> Register(ApplicationUser user)
        {
            user.Role = "Default";

            try
            {
                var result = await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, user.Role);
                return result;

            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new DirectoryNotFoundException();
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            var roles = await _userManager.GetRolesAsync(user);

            if (isPasswordValid && roles.Any())
            {
                var userContextExists = await ContextHasUser(user.Id);
                IdentityOptions _options = new IdentityOptions();

                if (!userContextExists)
                {
                    await _agileTeamsContext.ApplicationUsers.AddAsync(user);
                    await _agileTeamsContext.SaveChangesAsync();
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, roles.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationUserAuth.JWT_Token)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return token;

            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<ApplicationUser> GetUserAccount()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is null");
            }

            string userId = httpContext.User.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }

        public async Task<IdentityResult> UpdateUserAccount(string id, ApplicationUser user)
        {
            var applicationUser = await _userManager.FindByIdAsync(user.Id);

            if (applicationUser == null)
            {
                throw new DirectoryNotFoundException();
            }

            try
            {
                var result = await _userManager.UpdateAsync(user);

                return result;

            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException();
            }
        }

        private async Task<bool> ContextHasUser(string Id)
        {
            return await _agileTeamsContext.ApplicationUsers.AnyAsync(user => user.Id == Id);
        }
    }
}
