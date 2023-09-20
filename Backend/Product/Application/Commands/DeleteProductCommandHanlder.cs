using MediatR;
using Product.Application.Interfaces;
using SharedKernel.Types;

namespace Product.Application.Commands
{
    public record DeleteProductCommand(int ProductId) : IRequest<Response<string>>;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<string>>
    {
        private readonly IProductDbContext _context;

        public DeleteProductCommandHandler(IProductDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
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

            _context.Product.Remove(existingProduct);

            await _context.SaveChangesAsync(cancellationToken);

            return new Response<string>
            {
                Success = true,
            };
        }
    }
}
