using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService : IServiceBase<ProductDTO>
    {
        Task<ProductDTO> GetProductCategoryAsync(int? id);
    }
}
