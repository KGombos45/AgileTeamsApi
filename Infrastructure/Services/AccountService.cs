
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities.AgileTeams;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Services.Users;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly AgileTeamsContext _agileTeamsContext;
        private readonly ApplicationSettings _applicationSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AccountService(UserManager<ApplicationUser> userManager, 
            AgileTeamsContext agileTeamsContext, 
            IOptions<ApplicationSettings> applicationSettings, 
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _userManager = userManager;
            _agileTeamsContext = agileTeamsContext;
            _applicationSettings = applicationSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Register(RegistrationRequest user)
        {
            var existingUser = await _userManager.FindByNameAsync(user.UserName);
            var existingEmail = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already registered.");
            }

            if (existingEmail != null) {
                throw new InvalidOperationException("Email already register.");
            }

            var applicationUser = _mapper.Map<ApplicationUser>(user);

            var result = await _userManager.CreateAsync(applicationUser, user.Password);
            await _userManager.AddToRoleAsync(applicationUser, applicationUser.Role);

            return result;
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

            if (isPasswordValid)
            {
                var userContextExists = await ContextHasUser(user.Id);
                IdentityOptions _options = new IdentityOptions();

                if (!userContextExists)
                {
                    await _agileTeamsContext.ApplicationUsers.AddAsync(user);
                    await _agileTeamsContext.SaveChangesAsync();
                }

                var userRole = roles.FirstOrDefault();

                if (string.IsNullOrEmpty(userRole))
                {
                    throw new InvalidOperationException("User does not have any roles assigned.");
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName), 
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),  
                    new Claim(ClaimTypes.Role, userRole)
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(20), 
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.JWT_Token)),
                        SecurityAlgorithms.HmacSha256Signature)
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

        public async Task<ApplicationUserDto> GetLoggedInUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null || httpContext.User == null)
            {
                throw new InvalidOperationException("HttpContext is null");
            }

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new InvalidOperationException("User claim is null");
            }

            string userId = userIdClaim.Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new DirectoryNotFoundException("User could not be found");
            }

            return _mapper.Map<ApplicationUserDto>(user);
        }

        public async Task<IdentityResult> UpdateUserAccount(string id, ApplicationUserDto user)
        {
            var applicationUser = await _userManager.FindByIdAsync(user.Id);

            if (applicationUser == null)
            {
                throw new DirectoryNotFoundException();
            }

            _mapper.Map(user, applicationUser);

            try
            {
                var result = await _userManager.UpdateAsync(applicationUser);
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
