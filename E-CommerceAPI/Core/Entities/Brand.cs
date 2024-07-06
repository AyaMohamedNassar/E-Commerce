namespace Core.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Category>? Categories { get; set; } = new List<Category>();
        public virtual List<Product>? Products { get; set; } = new List<Product>();
    }
}
