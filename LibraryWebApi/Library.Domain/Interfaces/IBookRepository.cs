using Library.Domain.Helpers;
using Library.Domain.Entities;
using Library.Domain.Entities.BookDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Library.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(QueryObject query);

        Task<Book?> GetByIdAsync(int id);

        Task<Book?> CreateAsync(Book book);

        Task<Book?> UpdateAsync(int id, BookUpdateDto bookUpdatingDto);

        Task<Book?> DeleteAsync(int id);

        Task<Author?> FindAuthorByName(string name);

        Task<Book?> TakeBook(string bookTitle, string userId);

        Task<List<Book>?> GetTakenBooks(string userId, QueryObject query);

        Task<Book?> ReturnBook(string bookTitle, string userId);
    }
}
