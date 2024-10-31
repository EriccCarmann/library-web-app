using Library.Domain.Helpers;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository.UnitOfWork
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        private DbSet<T> table;

        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public async Task<List<T>> GetAll(QueryObject query)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await table.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            var item = await table.FindAsync(id);

            if (item == null) return null;

            return item;
        }

        public async Task<T?> CreateAsync(T obj)
        {
            await table.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<T?> DeleteAsync(object id)
        {
            var item = await table.FindAsync(id);

            if (item == null) return null;

            table.Remove(item);

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<T?> UpdateAsync(object id, T obj)
        {
            var item = await table.FindAsync(obj);

            if (item == null) return null;

            _context.Entry(item).CurrentValues.SetValues(obj);

            await _context.SaveChangesAsync();

            return item;
        }
    }
}
