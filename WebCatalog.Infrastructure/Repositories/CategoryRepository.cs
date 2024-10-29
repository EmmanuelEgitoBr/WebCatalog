using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Interfaces;
using WebCatalog.Infrastructure.Context;

namespace WebCatalog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> RemoveCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category != null) 
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
            }

            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
