using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.UseCases.AccountUseCases
{
    public class LogoutUseCase
    {
        private readonly SignInManager<LibraryUser> _signInManager;

        public LogoutUseCase(SignInManager<LibraryUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
