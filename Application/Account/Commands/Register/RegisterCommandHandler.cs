using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IdentityResult>
    {
        private readonly IAccountService _accountsService;

        public RegisterCommandHandler(IAccountService accountsService)
        {
            _accountsService = accountsService;
        }

        public async Task<IdentityResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = await _accountsService.Register(request.User);

            return response;
        }
    }
}
