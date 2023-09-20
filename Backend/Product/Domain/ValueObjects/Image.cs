using SharedKernel.Interfaces;

namespace Product.Domain.ValueObjects;

public class Image : AuditableEntity
{
    public string Base64 { get; set; }

    public Entities.Product Product { get; set; }
}
