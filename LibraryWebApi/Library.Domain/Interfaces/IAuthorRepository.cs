using Library.Domain.Entities;
using Library.Domain.Entities.AuthorDTOs;
using Library.Domain.Helpers;

namespace Library.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync(QueryObject query);

        Task<Author?> GetByIdAsync(int id);

        Task<Author> CreateAsync(Author author);

        Task<Author?> UpdateAsync(int id, AuthorUpdateDto authorUpdateDto);

        Task<Author?> DeleteAsync(int id);
    }
}