using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Library.Domain.Interfaces
{
    public interface IAccountRepository : IGenericRepository<LibraryUser> { }
}
