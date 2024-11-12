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

            if (await _unitOfWork.Account.FindUserByName(libraryUser.UserName) != null)
            {
                throw new LoginAlreadyExistsException($"Login {libraryUser.UserName} is already in use!");
            }

            var user = await _unitOfWork.Account.Register(libraryUser, registerDto.Password);

            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<ShowNewUserDto> Login(LoginDto loginDto)
        {
            if (await _unitOfWork.Account.FindUserByName(loginDto.UserName) == null)
            {
                throw new EntityNotFoundException($"User {loginDto.UserName} is not found in database.");
            }

            var result = await _unitOfWork.Account.Login(loginDto.UserName, loginDto.Password);

            if (!result.Succeeded)
            {
                throw new WrongPasswordException("Wrong password");
            }

            return new ShowNewUserDto
            {
                UserName = loginDto.UserName
            };
        }

        public async Task Logout()
        {
            await _unitOfWork.Account.Logout();
        }
    }
}
