using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<LibraryUser?> Register(LibraryUser libraryUser, string password);
        Task<LibraryUser?> Login(string name, string password);
        Task<Task> Logout();
    }
}
