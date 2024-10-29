using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCatalog.Application.DTOs;
using WebCatalog.Application.Interfaces;

namespace WebCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();

            if (categories == null)
            {
                return NotFound("Não foram encontradas categorias");
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound("Não foi encontrada a categoria");
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Não foi possível criar a categoria");
            }

            await _categoryService.CreateCategory(categoryDTO);

            //return new CreatedAtRouteResult("GetProductById", new { id = categoryDTO.Id }, categoryDTO);
            return Ok(categoryDTO);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null) { return BadRequest("Não foi possível atualizar a categoria"); }

            await _categoryService.UpdateCategory(categoryDTO);

            return Ok(categoryDTO);
        }

        [HttpDelete]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<CategoryDTO>> RemoveCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null) { return BadRequest("Não foi possível apagar a categoria"); }

            await _categoryService.RemoveCategory(categoryDTO);

            return Ok(categoryDTO);
        }
    }
}
