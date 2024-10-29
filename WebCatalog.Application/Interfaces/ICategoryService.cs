using WebCatalog.Application.DTOs;

namespace WebCatalog.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO> GetCategoryById(int id);
        Task CreateCategory(CategoryDTO categoryDTO);
        Task UpdateCategory(CategoryDTO categoryDTO);
        Task RemoveCategory(CategoryDTO categoryDTO);
    }
}
