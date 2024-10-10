using Library.Infrastructure.Persistence;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Entities.AuthorDTOs;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Author.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Author.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Author?> UpdateAsync(int id, AuthorUpdateDto authorUpdateDto)
        {
            var existingAuthor = await _context.Author.FirstOrDefaultAsync(x => x.Id == id);

            if (existingAuthor == null) 
            {
                return null;
            }

            existingAuthor.FirstName = authorUpdateDto.FirstName;
            existingAuthor.LastName = authorUpdateDto.LastName;
            existingAuthor.DateOfBirth = authorUpdateDto.DateOfBirth;
            existingAuthor.Country = authorUpdateDto.Country;

            await _context.SaveChangesAsync();

            return existingAuthor;
        }
    }
}
