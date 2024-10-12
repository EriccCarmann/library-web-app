using Library.Domain.Entities;
using Library.Infrastructure.Controllers;
using Library.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using FakeItEasy;
using Library.Domain.Entities.LibraryUserDTOs;
using FluentAssertions;

namespace LibraryWebApi.Tests.ControllersTests
{
    public class AccountControllerTests
    {
        private readonly UserManager<LibraryUser> _userManager;
        private readonly SignInManager<LibraryUser> _signInManager;
        private readonly ApplicationDBContext _context;

        private readonly AccountController _accountController;

        public AccountControllerTests()
        {
            _userManager = A.Fake<UserManager<LibraryUser>>();
            _signInManager = A.Fake<SignInManager<LibraryUser>>();

            _accountController = new AccountController(_userManager, _signInManager, _context);
        }

        [Fact]
        public void AccountController_Register_LibraryUser()
        {
            RegisterDto registerDto = new RegisterDto()
            {
                UserName = "Test",
                Email = "Test",
                Password = "Test"
            };

            var result = _accountController.Register(registerDto);

            result.Should().NotBeNull();
        }

        [Fact]
        public void AccountController_Login_LibraryUser()
        {
            LibraryUser user = new LibraryUser()
            {
                Id = "4di2ynt8",
                UserName = "Test",
                Email = "Test",
                PasswordHash = "Test"
            };

            LoginDto loginDto = new LoginDto()
            {
                UserName = "Test",
                Password = "Test"
            };

            var result = _accountController.Login(loginDto, _signInManager);

            result.Should().NotBeNull();
        }
    }
}
