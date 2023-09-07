using MediatR;
using Product.Application.Interfaces;
using SharedKernel.Types;

namespace Product.Application.Commands
{
    public record UpdateProductCommand(int ProductId, string Title, string Description, string Condition, string Price, int CategoryId) : IRequest<Response<string>>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<string>>
    {
        private readonly IProductDbContext _context;

        public UpdateProductCommandHandler(IProductDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _context.Product.FindAsync(request.ProductId);

            if (existingProduct == null)
            {
                return new Response<string>
                {
                    Success = false,
                    ErrorMessage = "Product not found."
                };
            }

            existingProduct = new Domain.Entities.Product
            {
                Title = request.Title,
                Description = request.Description,
                Condition = request.Condition,
                Price = request.Price,
                CategoryId = request.CategoryId
            };

            await _context.SaveChangesAsync(cancellationToken);

            return new Response<string>
            {
                Success = true,
                Data = "Product updated successfully."
            };
        }
    }
}
