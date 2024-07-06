using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceAPI.DTOs
{
    public class ProductUploadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
       
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        
        public int CategoryId { get; set; }
       
        public int BrandId { get; set; }
        
    }
}
