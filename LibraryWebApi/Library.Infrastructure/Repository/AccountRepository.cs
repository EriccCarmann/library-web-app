using Library.Domain.Entities;
using Library.Domain.Exceptions;
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

        public async Task<LibraryUser?> Register(LibraryUser libraryUser, string password)
        {
            var newUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == libraryUser.UserName.ToLower());

            if (newUser != null)
            {
                throw new LoginAlreadyExistsException($"Login {libraryUser.UserName} is already in use!");
            }

            var createUser = await _userManager.CreateAsync(libraryUser, password);

            if (!createUser.Succeeded)
            {
                throw new UserCreationException($"User {libraryUser.UserName} was not created");
            }

            var roleResult = await _userManager.AddClaimAsync(libraryUser, new Claim(ClaimTypes.Role, "User"));

            return libraryUser;
        }

        public async Task<LibraryUser?> Login(string name, string password) 
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == name.ToLower());

            if (user is null)
            {
                throw new EntityNotFoundException($"User {name} is not found in database.");
            }

            var result = await _signInManager.PasswordSignInAsync(name, password, false, false);

            if (!result.Succeeded)
            {
                throw new WrongPasswordException("Wrong password");
            }

            return user;
        }

        public async Task<Task> Logout()
        {
            var result = _signInManager.SignOutAsync();
            return result;
        }
    }
}
