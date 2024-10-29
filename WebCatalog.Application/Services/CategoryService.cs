using AutoMapper;
using WebCatalog.Application.DTOs;
using WebCatalog.Application.Interfaces;
using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Interfaces;

namespace WebCatalog.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreateCategory(CategoryDTO categoryDTO)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDTO);

            await _categoryRepository.CreateCategoryAsync(categoryEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var categoriesEntity = await _categoryRepository.GetAllCategoriesAsync();

            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var categoryEntity = await _categoryRepository.GetCategoryByIdAsync(id);

            return _mapper.Map<CategoryDTO>(categoryEntity);
        }

        public async Task RemoveCategory(CategoryDTO categoryDTO)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDTO);

            await _categoryRepository.RemoveCategoryAsync(categoryEntity.Id);
        }

        public async Task UpdateCategory(CategoryDTO categoryDTO)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDTO);

            await _categoryRepository.UpdateCategoryAsync(categoryEntity);
        }
    }
}
