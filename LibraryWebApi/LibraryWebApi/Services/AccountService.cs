using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Helpers;
using Library.Infrastructure.UnitOfWork;
using LibraryWebApi.DTOs.LibraryUserDTOs;
using LibraryWebApi.Validators;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Services
{
    public class AccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LibraryUserValidator _libraryUserValidator;

        public AccountService(
            IUnitOfWork unitOfWork,
            LibraryUserValidator libraryUserValidator)
        {
            _unitOfWork = unitOfWork;
            _libraryUserValidator = libraryUserValidator;
        }

        public async Task<IEnumerable<LibraryUser>> GetAll([FromQuery] QueryObject queryObject)
        {
            return await _unitOfWork.Account.GetAllAsync(queryObject);
        }

        public async Task<LibraryUser> Register([FromBody] RegisterDto registerDto)
        {
            var libraryUser = new LibraryUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };

            if (!_libraryUserValidator.Validate(libraryUser).IsValid)
            {
                throw new DataValidationException("Input data is invalid");
            }

            var user = await _unitOfWork.Account.Register(libraryUser, registerDto.Password);

            return user;
        }

        public async Task<ShowNewUserDto> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.Account.Login(loginDto.UserName, loginDto.Password);

            return new ShowNewUserDto
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task Logout()
        {
            await _unitOfWork.Account.Logout();
        }
    }
}
