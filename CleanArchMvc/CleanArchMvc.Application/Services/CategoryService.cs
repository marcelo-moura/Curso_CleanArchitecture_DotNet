using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(await _categoryRepository.GetAllAsync());
        }

        public async Task<CategoryDTO> GetByIdAsync(int? id)
        {
            return _mapper.Map<CategoryDTO>(await _categoryRepository.GetByIdAsync(id));
        }

        public async Task CreateAsync(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.CreateAsync(category);
        }

        public async Task UpdateAsync(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int? id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}
