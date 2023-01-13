namespace CleanArchMvc.Application.Interfaces
{
    public interface IServiceBase<TEntityDto> where TEntityDto : class
    {
        Task<IEnumerable<TEntityDto>> GetAllAsync();
        Task<TEntityDto> GetByIdAsync(int? id);
        Task CreateAsync(TEntityDto entity);
        Task UpdateAsync(TEntityDto entity);
        Task DeleteAsync(int? id);
    }
}
