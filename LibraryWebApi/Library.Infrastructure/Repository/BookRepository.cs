using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Library.Infrastructure.Persistence;
using Library.Domain.Helpers;

namespace Library.Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDBContext _context;

        public BookRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<Book?> GetByIdISBN(string ISBN)
        {
            return await _context.Book.FirstOrDefaultAsync(x => x.ISBN == ISBN);
        }

        public async Task<Book?> TakeBook(string bookTitle, string userId)
        {
            var existingBook = await _context.Book.FirstOrDefaultAsync(x => x.Title.ToLower() == bookTitle.ToLower());

            if (existingBook == null) return null;
            if (existingBook.IsTaken == true) return null;

            existingBook.IsTaken = true;
            existingBook.UserId = userId;
            existingBook.TakeDateTime = DateTime.UtcNow;
            existingBook.ReturnDateTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return existingBook;
        }

        public async Task<List<Book>?> GetTakenBooks(string userId, QueryObject query)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await _context.Book.Where(b => b.UserId == userId).Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Book?> ReturnBook(string bookTitle, string userId)
        {
            var existingBook = await _context.Book.FirstOrDefaultAsync(b => b.Title == bookTitle && b.UserId == userId);

            if (existingBook == null) return null;

            existingBook.IsTaken = false;
            existingBook.UserId = null;
            existingBook.TakeDateTime = null;
            existingBook.ReturnDateTime = null;

            await _context.SaveChangesAsync();

            return existingBook;
        }

        public async Task<Book?> AddCover(string bookTitle, byte[] file)
        {
            var existingBook = await _context.Book.FirstOrDefaultAsync(b => b.Title == bookTitle);

            if (file == null && file.Length <= 0 && existingBook == null)
            {
                return null;
            }

            existingBook.Cover = file;

            _context.SaveChanges();

            return existingBook;

        }
    }
}
