using Library.Domain.Helpers;

namespace Library.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(QueryObject query);
        Task<T?> GetByIdAsync(object id);
        Task<T?> CreateAsync(T obj);
        Task<T?> UpdateAsync(object id, T obj);
        Task<T?> DeleteAsync(object id);     
    }
}
