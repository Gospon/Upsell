using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Commands;
using Product.Application.Queries;

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
        public async Task<ActionResult> GetProducts()
        {
            return Ok(_mediator.Send(new GetProductsQuery()));
        }

        [HttpPost("product")]
        public async Task<ActionResult> AddProduct(AddProductCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("products/{productId}")]
        public async Task<ActionResult> UpdateProduct(UpdateProductCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("products/{productId}")]
        public async Task<ActionResult> DeleteProduct(DeleteProductCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
