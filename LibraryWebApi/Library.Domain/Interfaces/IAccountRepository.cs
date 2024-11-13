using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Library.Domain.Interfaces
{
    public interface IAccountRepository : IGenericRepository<LibraryUser>
    {
        Task<LibraryUser?> FindUserByName(string name);
        Task<IdentityResult?> Register(LibraryUser libraryUser, string password);
        Task<IdentityResult?> AddUserClaim(LibraryUser libraryUser);
        Task<SignInResult?> Login(string name, string password);      
        Task<Task> Logout();
    }
}
