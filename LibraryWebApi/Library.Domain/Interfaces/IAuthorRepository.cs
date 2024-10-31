using Library.Domain.Entities;
using Library.Domain.Helpers;

namespace Library.Domain.Interfaces
{
    public interface IAuthorRepository 
    {
        Task<Author?> FindAuthorByName(string name);
    }
}