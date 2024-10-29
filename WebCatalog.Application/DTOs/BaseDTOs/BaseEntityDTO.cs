using System.Text.Json.Serialization;

namespace WebCatalog.Application.DTOs.BaseDTOs
{
    public abstract class BaseEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
