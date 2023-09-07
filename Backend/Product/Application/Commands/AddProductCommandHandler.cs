using MediatR;
using Product.Application.Interfaces;
using SharedKernel.Types;

namespace Product.Application.Commands;

public record AddProductCommand(string Title, string Description, string Condition, string Price, int CategoryId) : IRequest<Response<int>>;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Response<int>>
{
    private readonly IProductDbContext _context;
    public AddProductCommandHandler(IProductDbContext context)
    {
        _context = context;
    }

    public async Task<Response<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product()
        {
            Title = request.Title,
            Description = request.Description,
            Condition = request.Condition,
            Price = request.Price,
            CategoryId = request.CategoryId
        };

        _context.Product.Add(product);

        await _context.SaveChangesAsync(cancellationToken);

        return new Response<int>
        {
            Success = true,
            Data = product.Id
        };

    }
}
