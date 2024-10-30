using Library.Infrastructure.Persistence;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Helpers;

namespace Library.Infrastructure.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDBContext _context;

        public AuthorRepository(ApplicationDBContext context) 
        { 
            _context = context;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            await _context.Author.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author?> DeleteAsync(int id)
        {
            var author = await _context.Author.FirstOrDefaultAsync(x => x.Id == id);

            if (author == null) 
            {
                return null;
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return author;
        }

        public async Task<List<Author>> GetAllAsync(QueryObject query)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await _context.Author.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Author.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Author?> UpdateAsync(int id, Author updateAuthor)
        {
            var existingAuthor = await _context.Author.FirstOrDefaultAsync(x => x.Id == id);

            if (existingAuthor == null) 
            {
                return null;
            }

            existingAuthor.FirstName = updateAuthor.FirstName;
            existingAuthor.LastName = updateAuthor.LastName;
            existingAuthor.DateOfBirth = updateAuthor.DateOfBirth;
            existingAuthor.Country = updateAuthor.Country;

            await _context.SaveChangesAsync();

            return existingAuthor;
        }
    }
}
