using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IAuthorBookRepository
    {
        Task<List<AuthorBook>> GetAllAsync();
        Task<List<Book>> GetBooksByAuthorAsync(string name);
    }
}
