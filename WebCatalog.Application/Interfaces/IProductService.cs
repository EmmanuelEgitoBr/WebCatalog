using WebCatalog.Application.DTOs;
using WebCatalog.Domain.Models;
using X.PagedList;

namespace WebCatalog.Application.Interfaces
{
    public interface IProductService
    {
        Task<IPagedList<ProductDTO>> GetAllProducts(PaginationParameters productParams);
        Task<ProductDTO> GetProductById(int id);
        Task<IEnumerable<ProductDTO>> GetProductsByCategoryId(int categoryId, PaginationParameters productsParams);
        Task CreateProduct(ProductDTO productDTO);
        Task UpdateProduct(ProductDTO productDTO);
        Task RemoveProduct(ProductDTO productDTO);
    }
}
