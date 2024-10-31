using Library.Domain.Helpers;

namespace Library.Infrastructure.Repository.UnitOfWork
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll(QueryObject query);
        Task<T?> GetByIdAsync(object id);
        Task<T?> CreateAsync(T obj);
        Task<T?> DeleteAsync(object id);
        Task<T?> UpdateAsync(object id, T obj);   
    }
}
