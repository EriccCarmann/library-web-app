﻿using Library.Domain.Helpers;
using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book?> GetByISBN(string ISBN);

        Task<Book?> GetByTitle(string title);

        Task<Book?> TakeBook(Book book, string userId);

        Task<List<Book>?> GetTakenBooks(string userId, QueryObject query);

        Task<Book?> ReturnBook(Book book, string userId);

        Book AddCover(Book book, byte[] cover);
    }
}
