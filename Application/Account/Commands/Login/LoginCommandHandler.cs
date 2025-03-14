using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Accounts.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IAccountService _accountsService;
        public LoginCommandHandler(IAccountService accountsService)
        {
            _accountsService = accountsService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _accountsService.Login(request.UserName, request.Password);

            return response;
        }
    }
}
