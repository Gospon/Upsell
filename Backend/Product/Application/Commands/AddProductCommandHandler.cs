using MediatR;
using Product.Application.Interfaces;
using Product.Domain.ValueObjects;
using SharedKernel.Types;

namespace Product.Application.Commands;

public record AddProductCommand(string Title, string Description, string Condition, string Price, int CategoryId, List<string> Images) : IRequest<Response<int>>;

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
            CategoryId = request.CategoryId,
        };

        if (request.Images.Any())
        {
            product.Images = new List<Image>();

            foreach (var imageBase64 in request.Images)
            {
                var image = new Image
                {
                    Base64 = imageBase64,
                };

                product.Images.Add(image);
            }
        }

        _context.Product.Add(product);

        await _context.SaveChangesAsync(cancellationToken);

        return new Response<int>
        {
            Success = true,
            Data = product.Id
        };

    }
}
