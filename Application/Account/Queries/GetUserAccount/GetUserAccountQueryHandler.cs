using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Administration.Queries.GetUserProfiles;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.AgileTeams;
using MediatR;

namespace Application.Account.Queries.GetUserAccount
{
    public class GetUserAccountQueryHandler : IRequestHandler<GetUserAccountQuery, ApplicationUserDto>
    {
        private readonly IAccountService _accountsService;

        public GetUserAccountQueryHandler(IAccountService accountsService)
        {
            _accountsService = accountsService;
        }

        public async Task<ApplicationUserDto> Handle(GetUserAccountQuery request, CancellationToken cancellationToken)
        {
            var user = await _accountsService.GetLoggedInUser();

            return user;
        }
    }
}
