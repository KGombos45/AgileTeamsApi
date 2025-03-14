using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> Register(ApplicationUser user);
        public Task<string> Login(string username, string password);
        public Task<ApplicationUser> GetUserAccount();
        public Task<IdentityResult> UpdateUserAccount(string id, ApplicationUser user);
    }
}
