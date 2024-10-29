using AutoMapper;
using WebCatalog.Application.DTOs;
using WebCatalog.Application.Interfaces;
using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Interfaces;
using WebCatalog.Domain.Models;
using X.PagedList;

namespace WebCatalog.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task CreateProduct(ProductDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);

            await _productRepository.CreateProductAsync(productEntity);
        }

        public async Task<IPagedList<ProductDTO>> GetAllProducts(PaginationParameters productParams)
        {
            IPagedList<Product> productsEntity = await _productRepository.GetAllProductsAsync(productParams);

            //return _mapper.Map<IPagedList<ProductDTO>>(productsEntity);

            return MapPagedList(productsEntity);
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var productEntity = await _productRepository.GetProductByIdAsync(id);

            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategoryId(int categoryId, PaginationParameters productsParams)
        {
            var productsEntity = await _productRepository.GetProductsByCategoryIdAsync(categoryId, productsParams);

            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task RemoveProduct(ProductDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);

            await _productRepository.RemoveProductAsync(productEntity.Id);
        }

        public async Task UpdateProduct(ProductDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);

            await _productRepository.UpdateProductAsync(productEntity);
        }

        private IPagedList<ProductDTO> MapPagedList(IPagedList<Product> sourcePagedList)
        {
            // Mapeia os itens da lista de origem para o destino
            var destinationItems = sourcePagedList.Select(item => _mapper.Map<ProductDTO>(item)).ToList();

            // Cria uma nova IPagedList com base na lista mapeada
            return new StaticPagedList<ProductDTO>(destinationItems, sourcePagedList.PageNumber, sourcePagedList.PageSize, sourcePagedList.TotalItemCount);
        }
    }
}
