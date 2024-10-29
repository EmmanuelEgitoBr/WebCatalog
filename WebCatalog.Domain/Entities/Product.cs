using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebCatalog.Domain.Entities.Base;

namespace WebCatalog.Domain.Entities
{
    public class Product : BaseEntity
    {
        [StringLength(200)]
        public string ProductDescription { get; set; }
        
        
        [Column(TypeName ="decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public int Stock {  get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
