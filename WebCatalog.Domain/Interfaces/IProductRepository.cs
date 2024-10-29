using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Models;
using X.PagedList;

namespace WebCatalog.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IPagedList<Product>> GetAllProductsAsync(PaginationParameters productsParams);
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId, PaginationParameters productsParams);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<Product> RemoveProductAsync(int id);
    }
}
