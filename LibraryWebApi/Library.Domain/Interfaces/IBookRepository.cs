using Library.Domain.Entities;
using Library.Domain.Entities.BookDTOs;

namespace Library.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();

        Task<Book?> GetByIdAsync(int id);

        Task<Book> CreateAsync(Book book);

        Task<Book?> UpdateAsync(int id, BookUpdateDto bookUpdatingDto);

        Task<Book> DeleteAsync(int id);
    }
}
