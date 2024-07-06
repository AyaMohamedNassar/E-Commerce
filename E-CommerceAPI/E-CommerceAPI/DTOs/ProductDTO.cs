using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceAPI.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public int BrandId { get; set; }
    }
}
