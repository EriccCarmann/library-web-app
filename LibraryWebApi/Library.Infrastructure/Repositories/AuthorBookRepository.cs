using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class AuthorBookRepository : IAuthorBookRepository
    {
        private readonly ApplicationDBContext _context;

        public AuthorBookRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorBook>> GetAllAsync()
        {
            return await _context.AuthorBook.ToListAsync();
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(string name)
        {
            var author = await _context.Author
                .FirstOrDefaultAsync(a => (a.FirstName + " " + a.LastName).Contains(name));

            if (author == null) 
            {
                return null;
            }

            return await _context.AuthorBook.Where(u => u.AuthorId == author.Id)
                .Select(book => new Book
                {
                    Id = book.Book.Id,
                    Title = book.Book.Title,
                    Genre = book.Book.Genre,
                    Description = book.Book.Description,
                    ISBN = book.Book.ISBN,
                    IsTaken = book.Book.IsTaken
                }).ToListAsync();
        }
    }
}
