using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Queries;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<string>> GetCategories()
        {
            return Ok(await _mediator.Send(new GetCategoriesQuery()));
        }
    }
}
