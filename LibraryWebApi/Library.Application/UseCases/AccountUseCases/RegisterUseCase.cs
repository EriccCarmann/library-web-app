using Library.Application.DTOs.LibraryUserDTOs;
using Library.Application.Validators;
using Library.Domain.Entities;
using Library.Application.Exceptions;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.AccountUseCases
{
    public class RegisterUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LibraryUserValidator _libraryUserValidator;

        public RegisterUseCase(IUnitOfWork unitOfWork,
            LibraryUserValidator libraryUserValidator)
        {
            _unitOfWork = unitOfWork;
            _libraryUserValidator = libraryUserValidator;
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
    }
}
