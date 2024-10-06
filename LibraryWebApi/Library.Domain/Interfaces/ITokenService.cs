using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(LibraryUser libraryUser);
    }
}
