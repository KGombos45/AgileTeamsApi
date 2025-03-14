using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Administration.Commands.UpdateUserRole;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account.Commands.UpdateUserAccount
{
    public class UpdateUserAccountCommandHandler : IRequestHandler<UpdateUserAccountCommand, IdentityResult>
    {
        private readonly IAccountService _accountsService;

        public UpdateUserAccountCommandHandler(IAccountService accountsService)
        {
            _accountsService = accountsService;
        }

        public async Task<IdentityResult> Handle(UpdateUserAccountCommand request, CancellationToken cancellationToken)
        {
            var response = await _accountsService.UpdateUserAccount(request.User.Id, request.User);

            return response;
        }
    }
}
