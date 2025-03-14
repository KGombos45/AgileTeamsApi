
using Application.Common.Interfaces;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AdministrationService : IAdministrationService
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAgileTeamsContext _agileTeamsContext;

        public AdministrationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAgileTeamsContext agileTeamsContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _agileTeamsContext = agileTeamsContext;
        }

        public async Task<List<ApplicationUser>> GetUserProfiles()
        {
            var usersList = await _agileTeamsContext.ApplicationUsers.ToListAsync();

            return usersList;
        }

        public async Task<IdentityResult> UpdateUserRole(string id, ApplicationUser applicationUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = await _roleManager.FindByNameAsync(applicationUser.Role);
            var result = new IdentityResult();

            if (user == null) 
            {
                throw new DirectoryNotFoundException();
            }

            var oldRoles = await _userManager.GetRolesAsync(user);

            if (role == null || role.Name == null)
            {
                throw new DirectoryNotFoundException();
            }

            if (oldRoles != null)
            {
                result = await _userManager.RemoveFromRolesAsync(user, oldRoles.ToArray());

                _agileTeamsContext.ApplicationUsers.Update(applicationUser);
                await _agileTeamsContext.SaveChangesAsync();

                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
            }
            else
            {
                result = await _userManager.AddToRoleAsync(user, role.Name);
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
            return await _roleManager.Roles.Select(role => role.Name).ToListAsync();
        }

    }
}
