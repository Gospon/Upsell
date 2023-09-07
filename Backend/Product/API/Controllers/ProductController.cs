using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("product")]
        public async Task<ActionResult<string>> GetProducts()
        {
            return Ok();
        }

        [HttpPost("product")]
        public async Task<ActionResult<string>> AddProduct(Product.Domain.Entities.Product product)
        {
            return Ok();
        }
    }
}
