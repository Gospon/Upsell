using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using SharedKernel.Types;

namespace Product.Application.Queries;

public record GetProductsQuery() : IRequest<Response<IEnumerable<GetProductsQueryModel>>>;
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Response<IEnumerable<GetProductsQueryModel>>>
{
    private readonly IProductDbContext _context;
    public GetProductsQueryHandler(IProductDbContext context, IMapper mapper)
    {
        _context = context;
    }
    public async Task<Response<IEnumerable<GetProductsQueryModel>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Product.ToListAsync(cancellationToken);

        if (products.Any())
        {
            var categoriesQueryModel = products.Select(p => new GetProductsQueryModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Condition = p.Condition,
                Price = p.Price,
                CategoryId = p.CategoryId,
            });

            return new Response<IEnumerable<GetProductsQueryModel>> { Success = true, Data = categoriesQueryModel };
        }
        else
        {
            return new Response<IEnumerable<GetProductsQueryModel>> { Success = false, ErrorMessage = "No products were found." };
        }
    }
}

public class GetProductsQueryModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Condition { get; set; }

    public string Price { get; set; }

    public int CategoryId { get; set; }

    public List<string> Images { get; set; }

}
