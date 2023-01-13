using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<Product> GetProductCategoryAsync(int? id);
    }
}
