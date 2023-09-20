using Product.Domain.ValueObjects;
using SharedKernel.Interfaces;

namespace Product.Domain.Entities;

public class Product : AuditableEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Condition { get; set; }

    public string Price { get; set; }

    public int CategoryId { get; set; }

    public ICollection<Image> Images { get; set; }
}