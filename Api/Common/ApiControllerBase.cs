using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Common
{
    public class ApiControllerBase : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
