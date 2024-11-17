using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IGenerateToken
    {
        Task<string> CreateToken(LibraryUser libraryUser);
    }
}
