using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Infrastructure.Repository
{
    public class AccountRepository : GenericRepository<LibraryUser>, IAccountRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<LibraryUser> _userManager;
        private readonly SignInManager<LibraryUser> _signInManager;

        public AccountRepository(
            ApplicationDBContext context,
            SignInManager<LibraryUser> signInManager,
            UserManager<LibraryUser> userManager) : base(context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LibraryUser?> FindUserByName(string name)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == name.ToLower());
        }

        public async Task<IdentityResult?> Register(LibraryUser libraryUser, string password)
        {
            return await _userManager.CreateAsync(libraryUser, password);
        }

        public async Task<IdentityResult?> AddUserClaim(LibraryUser libraryUser)
        {
            return await _userManager.AddClaimAsync(libraryUser, new Claim(ClaimTypes.Role, "User"));
        }

        public async Task<SignInResult?> Login(string name, string password) 
        {
            var result = await _signInManager.PasswordSignInAsync(name, password, false, false);

            return result;
        }

        public async Task<Task> Logout()
        {
            var result = _signInManager.SignOutAsync();
            return result;
        }
    }
}
