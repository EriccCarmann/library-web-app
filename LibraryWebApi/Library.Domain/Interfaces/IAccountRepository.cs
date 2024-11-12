using Library.Domain.Entities;
using Library.Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Library.Domain.Interfaces
{
    public interface IAccountRepository : IGenericRepository<LibraryUser>
    {
        Task<LibraryUser?> FindUserByName(string name);
        Task<IdentityResult?> Register(LibraryUser libraryUser, string password);
        Task<IdentityResult?> AddUserClaim(LibraryUser libraryUser, string password);
        Task<SignInResult?> Login(string name, string password);
        Task<Task> Logout();
    }
}
