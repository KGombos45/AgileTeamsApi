using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UserLogin.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IdentityResult>
    {
        private readonly IUserLoginService _userLoginService;

        public RegisterCommandHandler(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        public async Task<IdentityResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var response = await _userLoginService.Register(request.User);

            return response;
        }
    }
}
