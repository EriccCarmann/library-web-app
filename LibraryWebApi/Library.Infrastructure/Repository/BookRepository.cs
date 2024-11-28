using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Library.Infrastructure.Persistence;
using Library.Domain.Helpers;

namespace Library.Infrastructure.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDBContext _context;

        public BookRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Book?> GetByISBN(string ISBN)
        {
            return await _context.Book.FirstOrDefaultAsync(x => x.ISBN == ISBN);
        }

        public async Task<Book?> GetByTitle(string title)
        {
            return await _context.Book.FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<Book?> TakeBook(string bookTitle, string userId)
        {
            var book = await _context.Book.FirstOrDefaultAsync(x => x.Title.ToLower() == bookTitle.ToLower());

            book.IsTaken = true;
            book.UserId = userId;
            book.TakeDateTime = DateTime.UtcNow;
            book.ReturnDateTime = DateTime.UtcNow.AddDays(7);

            return book;
        }

        public async Task<List<Book>?> GetTakenBooks(string userId, QueryObject query)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await _context.Book.Where(b => b.UserId == userId).Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Book?> ReturnBook(string bookTitle, string userId)
        {
            var book = await _context.Book.FirstOrDefaultAsync(b => b.Title == bookTitle && b.UserId == userId);

            book.IsTaken = false;
            book.UserId = null;
            book.TakeDateTime = null;
            book.ReturnDateTime = null;

            return book;
        }

        public Book AddCover(Book book, byte[] file)
        {
            book.Cover = file;

            return book;
        }
    }
}
