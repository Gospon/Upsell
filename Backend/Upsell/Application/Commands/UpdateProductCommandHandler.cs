using MediatR;
using Product.Application.Interfaces;
using Product.Domain.ValueObjects;
using SharedKernel.Types;

namespace Product.Application.Commands
{
    public record UpdateProductCommand(int ProductId, string Title, string Description, string Condition, string Price, int CategoryId, List<string> Images) : IRequest<Response<string>>;

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

            existingProduct.Title = request.Title;
            existingProduct.Description = request.Description;
            existingProduct.Condition = request.Condition;
            existingProduct.Price = request.Price;
            existingProduct.CategoryId = request.CategoryId;

            existingProduct.Images = new List<Image>();

            foreach (var updatedImageBase64 in request.Images)
            {
                var image = new Image
                {
                    Base64 = updatedImageBase64,
                };

                existingProduct.Images.Add(image);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return new Response<string>
            {
                Success = true,
            };
        }
    }
}
