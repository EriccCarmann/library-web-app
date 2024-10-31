﻿using Library.Domain.Helpers;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Library.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdISBN(string ISBN);

        Task<Book?> TakeBook(string bookTitle, string userId);

        Task<List<Book>?> GetTakenBooks(string userId, QueryObject query);

        Task<Book?> ReturnBook(string bookTitle, string userId);

        Task<Book?> AddCover(string bookTitle, byte[] cover);
    }
}
