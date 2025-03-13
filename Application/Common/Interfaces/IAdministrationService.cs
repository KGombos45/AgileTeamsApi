using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IAdministrationService
    {
        public Task<List<ApplicationUser>> GetUserProfiles();
        public Task<IdentityResult> UpdateUserRole(string id, ApplicationUser applicationUser);
        public Task<IdentityResult> DeleteUser(string id);
        public Task<List<string>> GetRoles();
    }
}
