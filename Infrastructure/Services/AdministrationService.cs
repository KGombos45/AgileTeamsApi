
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Azure.Core;
using Domain.Entities.AgileTeams;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AdministrationService : IAdministrationService
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAgileTeamsContext _agileTeamsContext;
        private readonly IMapper _mapper;

        public AdministrationService(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IAgileTeamsContext agileTeamsContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _agileTeamsContext = agileTeamsContext;
            _mapper = mapper;
        }

        public async Task<List<ApplicationUserDto>> GetUserProfiles()
        {
            var usersList = await _userManager.Users.ToListAsync();

            return _mapper.Map<List<ApplicationUserDto>>(usersList);
        }

        public async Task<IdentityResult> UpdateUserRole(string id, IdentityRoles role)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new DirectoryNotFoundException("User not found");
            }

            var roleExists = await _roleManager.RoleExistsAsync(role.GetDescription());

            if (!roleExists)
            {
                throw new DirectoryNotFoundException("Role does not exist");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!removeResult.Succeeded)
            {
                return removeResult;
            }

            var result = await _userManager.AddToRoleAsync(user, role.GetDescription());

            if (result.Succeeded)
            {
                user.Role = role.GetDescription();
                await _userManager.UpdateAsync(user);
            }

            return result;
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new DirectoryNotFoundException();
            }

            _agileTeamsContext.ApplicationUsers.Remove(user);

            await _agileTeamsContext.SaveChangesAsync();

            return await _userManager.DeleteAsync(user);
        }

        public async Task<List<string>> GetRoles()
        {
            var roles = await _roleManager.Roles.Select(role => role.Name).ToListAsync();

            return await _roleManager.Roles.Select(role => role.Name).ToListAsync();
        }

    }
}
