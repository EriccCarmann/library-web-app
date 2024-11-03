using Library.Domain.Entities;
using FakeItEasy;
using FluentAssertions;
using LibraryWebApi.Controllers;
using Library.Infrastructure.UnitOfWork;
using LibraryWebApi.DTOs.LibraryUserDTOs;

namespace LibraryWebApi.Tests.ControllersTests
{
    public class AccountControllerTests
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly AccountController _accountController;

        public AccountControllerTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();

            _accountController = new AccountController(_unitOfWork);
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
