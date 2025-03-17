using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AgileTeams;

namespace Application.Common.Models
{
    public class UserRegistrationDto: ApplicationUser
    {
        public string Password { get; set; }
    }
}
