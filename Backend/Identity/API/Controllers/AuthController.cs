using Identity.Application.Commands;
using Identity.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDTO request)
        {
            return Ok(await _mediator.Send(new RegisterUserCommand(request)));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            return Ok(await _mediator.Send(new LoginUserCommand(request)));
        }
    }
}
