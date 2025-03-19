using Application.Common.Models;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> Register(RegistrationRequest user);
        public Task<string> Login(string username, string password);
        public Task<ApplicationUserDto> GetLoggedInUser();
        public Task<IdentityResult> UpdateUserAccount(string id, ApplicationUserDto user);
    }
}
