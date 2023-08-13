using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace User.API.Controllers
{
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        public UserController(IMediator mediator) { _mediator = mediator; }
    }
}
