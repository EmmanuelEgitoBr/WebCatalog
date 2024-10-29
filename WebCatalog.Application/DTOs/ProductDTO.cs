using WebCatalog.Application.DTOs.BaseDTOs;

namespace WebCatalog.Application.DTOs
{
    public class ProductDTO : BaseEntityDTO
    {
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int CategoryId { get; set; }
    }
}
