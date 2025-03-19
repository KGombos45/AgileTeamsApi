using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Entities.AgileTeams;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IAdministrationService
    {
        public Task<List<ApplicationUserDto>> GetUserProfiles();
        public Task<IdentityResult> UpdateUserRole(string id, IdentityRoles role);
        public Task<IdentityResult> DeleteUser(string id);
        public Task<List<string>> GetRoles();
    }
}
