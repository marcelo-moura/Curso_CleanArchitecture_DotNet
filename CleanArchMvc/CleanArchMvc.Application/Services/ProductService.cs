using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetAllAsync());
        }

        public async Task<ProductDTO> GetByIdAsync(int? id)
        {
            return _mapper.Map<ProductDTO>(await _productRepository.GetByIdAsync(id));
        }

        public async Task<ProductDTO> GetProductCategoryAsync(int? id)
        {
            return _mapper.Map<ProductDTO>(await _productRepository.GetProductCategoryAsync(id));
        }

        public async Task CreateAsync(ProductDTO categoryDTO)
        {
            var product = _mapper.Map<Product>(categoryDTO);
            await _productRepository.CreateAsync(product);
        }

        public async Task UpdateAsync(ProductDTO categoryDTO)
        {
            var product = _mapper.Map<Product>(categoryDTO);
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int? id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
