using Library.Application.Exceptions;
using Library.Domain.Helpers;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repository
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
        public async Task<List<T>> GetAllAsync(QueryObject query)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await table.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            var item = await _context.Set<T>().FindAsync(id);

            if (item is null)
            {
                throw new EntityNotFoundException($"{typeof(T).Name} with ID {id} not found.");
            }

            return item;
        }

        public async Task<T?> CreateAsync(T obj)
        {
            await table.AddAsync(obj);
            return obj;
        }

        public async Task<T?> UpdateAsync(object id, T obj)
        {
            var item = await table.FindAsync(id);

            if (item is null)
            {
                throw new EntityNotFoundException($"{item} is not found in database.");
            }

            _context.Entry(item).CurrentValues.SetValues(obj);

            return item;
        }

        public async Task<T?> DeleteAsync(object id)
        {
            var item = await _context.Set<T>().FindAsync(id);

            if (item is null)
            {
                throw new EntityNotFoundException($"{item} is not found in database.");
            }

            table.Remove(item);

            return item;
        }
    }
}
