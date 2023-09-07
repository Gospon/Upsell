using Product.Domain.ValueObjects;
using SharedKernel.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities;

public class Product : AuditableEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Condition { get; set; }

    public string Price { get; set; }

    public int CategoryId { get; set; }

    [NotMapped]
    public List<FileData> ImageUpload { get; set; }
}
