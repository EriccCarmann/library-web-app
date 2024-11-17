using Library.Domain.Entities;
using FakeItEasy;
using FluentAssertions;
using LibraryWebApi.Controllers;
using LibraryWebApi.Services;
using Library.Application.DTOs.LibraryUserDTOs;

namespace LibraryWebApi.Tests.ControllersTests
{
    public class AccountControllerTests
    {
        private readonly AccountService _accountService;

        private readonly AccountController _accountController;

        public AccountControllerTests()
        {
            _accountService = A.Fake<AccountService>();

            _accountController = new AccountController(_accountService);
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

            var result = _accountController.Login(loginDto);

            result.Should().NotBeNull();
        }
    }
}
