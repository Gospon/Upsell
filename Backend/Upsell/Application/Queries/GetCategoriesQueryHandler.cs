using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using SharedKernel.Types;

namespace Product.Application.Queries;

public record GetCategoriesQuery() : IRequest<Response<IEnumerable<GetCategoriesQueryModel>>>;
public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Response<IEnumerable<GetCategoriesQueryModel>>>
{
    private readonly IProductDbContext _context;
    public GetCategoriesQueryHandler(IProductDbContext context)
    {
        _context = context;
    }
    public async Task<Response<IEnumerable<GetCategoriesQueryModel>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Category.ToListAsync(cancellationToken);

        var categoriesQueryModel = categories.Select(c => new GetCategoriesQueryModel
        {
            Id = c.Id,
            ParentCategoryId = c.ParentCategoryId,
            Name = c.Name,
            Image = c.Image
        });

        return new Response<IEnumerable<GetCategoriesQueryModel>> { Success = true, Data = categoriesQueryModel };
    }
}

public class GetCategoriesQueryModel
{
    public int Id { get; set; }
    public int? ParentCategoryId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
}