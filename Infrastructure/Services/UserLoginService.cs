using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.AgileTeams;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class UserLoginService : IUserLoginService
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly AgileTeamsContext _agileTeamsContext;
        private readonly ApplicationUserAuth _applicationUserAuth;


        public UserLoginService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AgileTeamsContext agileTeamsContext, ApplicationUserAuth applicationUserAuth)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _agileTeamsContext = agileTeamsContext;
            _applicationUserAuth = applicationUserAuth;
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

        private async Task<bool> ContextHasUser(string Id)
        {
            return await _agileTeamsContext.ApplicationUsers.AnyAsync(user => user.ID == Id);
        }
    }
}
