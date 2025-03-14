using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.UserLogin.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserLoginService _userLoginService;
        public LoginCommandHandler(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _userLoginService.Login(request.UserName, request.Password);

            return response;
        }
    }
}
