namespace Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Product>? Products { get; set; } = new List<Product>();
        public virtual List<Brand>? Brands { get; set; }
    }
}
