using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebCatalog.Application.DTOs;
using WebCatalog.Application.Interfaces;
using WebCatalog.Domain.Models;
using X.PagedList;

namespace WebCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IPagedList<ProductDTO>>> GetAllProducts([FromQuery] PaginationParameters productsParams)
        {
            _logger.LogInformation( "########## ----- CONSULTANDO PRODUTOS ------ ##########");
            
            var products = await _productService.GetAllProducts(productsParams);

            var metadata = new
            {
                products.Count,
                products.PageSize,
                products.PageCount,
                products.TotalItemCount,
                products.HasNextPage,
                products.HasPreviousPage
            };
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            if (products == null)
            {
                return NotFound("Não foram encontrados produtos");
            }

            return Ok(products);
        }

        [HttpGet("GetByCategoryId/{categoryId:int:min(1)}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProductsByCategoryId(int categoryId, 
                                                                                            [FromQuery] PaginationParameters productsParams)
        {
            var products = await _productService.GetProductsByCategoryId(categoryId, productsParams);

            if (products == null)
            {
                return NotFound("Não foram encontrados produtos para esta categoria");
            }

            return Ok(products);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound("Não foi encontrado o produto");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO productDTO)
        {
            if(productDTO == null)
            {
                return BadRequest("Não foi possível criar o produto");
            }

            await _productService.CreateProduct(productDTO);

            //return new CreatedAtRouteResult("GetProductById", new { id = productDTO.Id }, productDTO);
            return Ok(productDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(ProductDTO productDTO)
        {
            if(productDTO == null) { return BadRequest("Não foi possível atualizar o produto"); }

            await _productService.UpdateProduct(productDTO);

            return Ok(productDTO);
        }

        [HttpDelete]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<ProductDTO>> RemoveProduct(ProductDTO productDTO)
        {
            if (productDTO == null) { return BadRequest("Não foi possível apagar o produto"); }

            await _productService.RemoveProduct(productDTO);

            return Ok(productDTO);
        }
    }
}
