using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebCatalog.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        
        [Required]
        public string Name { get; protected set; }
        
        [Required]
        [Url]
        public string ImageUrl { get; protected set; }
    }
}
