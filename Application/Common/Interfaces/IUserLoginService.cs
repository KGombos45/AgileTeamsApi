using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Models;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IUserLoginService
    {
        public Task<IdentityResult> Register(ApplicationUser user);
        public Task<string> Login(string username, string password);
    }
}
