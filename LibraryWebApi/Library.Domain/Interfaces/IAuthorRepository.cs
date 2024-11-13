using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<Author?> FindAuthorByName(string name);
    }
}