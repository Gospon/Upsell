namespace Product.Application.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public int ParentCategoryId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
