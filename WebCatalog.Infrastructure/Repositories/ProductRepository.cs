using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Interfaces;
using WebCatalog.Domain.Models;
using WebCatalog.Infrastructure.Context;
using X.PagedList;

namespace WebCatalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IPagedList<Product>> GetAllProductsAsync(PaginationParameters productsParams)
        {
            return await _context.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .AsQueryable()
                .ToPagedListAsync(productsParams.PageNumber, productsParams.PageSize);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId, PaginationParameters productsParams)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .OrderBy(p => p.Name)
                .Skip((productsParams.PageNumber - 1) * productsParams.PageSize)
                .Take(productsParams.PageSize)
                .ToListAsync();
        }

        public async Task<Product> RemoveProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
