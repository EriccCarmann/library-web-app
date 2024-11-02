using Library.Domain.Entities;
using Library.Domain.Interfaces.UnitOfWork;

namespace Library.Domain.Interfaces
{
    public interface IAccountRepository : IGenericRepository<LibraryUser>
    {
        Task<LibraryUser?> Register(LibraryUser libraryUser, string password);
        Task<LibraryUser?> Login(string name, string password);
        Task<Task> Logout();
    }
}
