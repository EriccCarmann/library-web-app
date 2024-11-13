using Library.Application.Validators;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Helpers;
using Library.Application.DTOs.LibraryUserDTOs;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Interfaces;

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

            var resultUser = await _unitOfWork.Account.Register(libraryUser, registerDto.Password);

            if (!resultUser.Succeeded)
            {
                throw new UserCreationException($"User {libraryUser.UserName} was not created");
            }

            var resultClaim = await _unitOfWork.Account.AddUserClaim(libraryUser);

            if (!resultClaim.Succeeded)
            {
                throw new AddClaimException($"User {libraryUser.UserName} was not assigned a claim");
            }

            await _unitOfWork.SaveChangesAsync();

            return libraryUser;
        }

        public async Task<ShowNewUserDto> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.Account.FindUserByName(loginDto.UserName);

            if (user == null)
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
