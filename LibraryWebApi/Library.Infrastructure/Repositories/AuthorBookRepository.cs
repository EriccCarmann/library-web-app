/*using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<AuthorBook> CreateBooksAuthorsAsync(string authorName, string bookName)
        {
            var author = await _context.Author
                .FirstOrDefaultAsync(a => (a.FirstName.ToLower() + " " + a.LastName.ToLower()).Contains(authorName.ToLower()));

            var book = await _context.Book
                .FirstOrDefaultAsync(b => b.Title.ToLower().Contains(bookName.ToLower()));

            if (author == null || book == null)
            {
                return null;
            }

            var all = await GetAllAsync();

            if (all.Any(e => e.AuthorId == author.Id) 
                && all.Any(e => e.BookId == book.Id))
            {
                return null;
            }

            var authorBook = new AuthorBook
            {
                AuthorId = author.Id,
                BookId = book.Id,
            };
           
            await _context.AuthorBook.AddAsync(authorBook);

            await _context.SaveChangesAsync();

            return authorBook;
        }

        public async Task<List<AuthorBook>> GetAllAsync()
        {
            return await _context.AuthorBook.ToListAsync();
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(string name)
        {
            var author = await _context.Author
                .FirstOrDefaultAsync(a => (a.FirstName.ToLower() + " " + a.LastName.ToLower()).Contains(name.ToLower()));

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
}*/
