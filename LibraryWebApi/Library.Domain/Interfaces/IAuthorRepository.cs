using Library.Domain.Entities;
using Library.Domain.Helpers;
using Library.Domain.Interfaces.UnitOfWork;

namespace Library.Domain.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<Author?> FindAuthorByName(string name);
    }
}