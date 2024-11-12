﻿using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Library.Infrastructure.Persistence;
using Library.Domain.Helpers;
using Library.Domain.Exceptions;

namespace Library.Infrastructure.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDBContext _context;

        public BookRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Book?> GetByIdISBN(string ISBN)
        {
            var existingBook = await _context.Book.FirstOrDefaultAsync(x => x.ISBN == ISBN);

            if (existingBook == null)
            {
                throw new EntityNotFoundException($"Book with ISBN {ISBN} was not found");
            }

            return existingBook;
        }

        public async Task<Book?> TakeBook(string bookTitle, string userId)
        {
            var existingBook = await _context.Book.FirstOrDefaultAsync(x => x.Title.ToLower() == bookTitle.ToLower());

            if (existingBook == null)
            {
                throw new EntityNotFoundException($"Book name {bookTitle} was not found");
            }

            if (existingBook.IsTaken == true)
            {
                throw new BookTakenException($"Book {bookTitle} was already taken");
            }

            existingBook.IsTaken = true;
            existingBook.UserId = userId;
            existingBook.TakeDateTime = DateTime.UtcNow;
            existingBook.ReturnDateTime = DateTime.UtcNow.AddDays(7);

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

            if (existingBook == null)
            {
                throw new EntityNotFoundException($"Book name {bookTitle} was not found");
            }
            
            existingBook.IsTaken = false;
            existingBook.UserId = null;
            existingBook.TakeDateTime = null;
            existingBook.ReturnDateTime = null;

            return existingBook;
        }

        public async Task<Book?> AddCover(string bookTitle, byte[] file)
        {
            var existingBook = await _context.Book.FirstOrDefaultAsync(b => b.Title == bookTitle);

            if (file == null && file.Length <= 0 && existingBook == null)
            {
                throw new InvalidCoverImageException("Invalid Image File");
            }

            existingBook.Cover = file;

            return existingBook;

        }
    }
}
