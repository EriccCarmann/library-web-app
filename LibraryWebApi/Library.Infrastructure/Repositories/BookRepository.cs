﻿using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Entities.BookDTOs;
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

        public async Task<List<Book>> GetAllAsync(QueryObject query)
        {
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await _context.Book.Skip(skipNumber).Take(query.PageSize).ToListAsync();         
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Book.FindAsync(id);
        }

        public async Task<Author?> FindAuthorByName(string? name)
        {
            var author = await _context.Author
                .FirstOrDefaultAsync(a => (a.FirstName.ToLower() + " " + a.LastName.ToLower()).Contains(name));

            return author;
        }

        public async Task<Book> CreateAsync(Book book)
        {
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> DeleteAsync(int? id)
        {
            var existingBook = await _context.Book.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBook == null) 
            {
                return null;
            }

            _context.Book.Remove(existingBook);
            await _context.SaveChangesAsync();

            return existingBook;
        }

        public async Task<Book?> UpdateAsync(int id, BookUpdateDto bookUpdatingDto)
        {
            var existingBook = await _context.Book.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBook == null) 
            {
                return null;
            }

            existingBook.Title = bookUpdatingDto.Title;
            existingBook.Genre = bookUpdatingDto.Genre;
            existingBook.Description = bookUpdatingDto.Description;
            existingBook.ISBN = bookUpdatingDto.ISBN;
            existingBook.IsTaken = bookUpdatingDto.IsTaken;
            existingBook.AuthorId = bookUpdatingDto.AuthorId;

            await _context.SaveChangesAsync();

            return existingBook;
        }

        public async Task<Book> DeleteAsync(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
            {
                return null;
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }
    }
}
