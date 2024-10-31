using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Infrastructure.Repository
{
    public class AccounRepository : IAccountRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<LibraryUser> _userManager;
        private SignInManager<LibraryUser> _signInManager;

        public AccounRepository(
            ApplicationDBContext context,
            SignInManager<LibraryUser> signInManager,
            UserManager<LibraryUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LibraryUser?> Register(LibraryUser libraryUser, string password)
        {
            var createUser = await _userManager.CreateAsync(libraryUser, password);

            if (!createUser.Succeeded) return null;

            var roleResult = await _userManager.AddClaimAsync(libraryUser, new Claim(ClaimTypes.Role, "User"));

            if (!roleResult.Succeeded) return null;

            return libraryUser;
        }

        public async Task<LibraryUser?> Login(string name, string password) 
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == name.ToLower());

            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(name, password, false, false);

            if (!result.Succeeded) return null;

            return user;
        }

        public async Task<Task> Logout()
        {
            var result = _signInManager.SignOutAsync();
            return result;
        }
    }
}
