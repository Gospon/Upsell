using SharedKernel.Interfaces;

namespace Product.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}