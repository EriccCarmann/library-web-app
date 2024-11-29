using Library.Application.DTOs.LibraryUserDTOs;
using Library.Application.Validators;
using Library.Domain.Entities;
using Library.Application.Exceptions;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Application.UseCases.AccountUseCases
{
    public class RegisterUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LibraryUserValidator _libraryUserValidator;
        private readonly UserManager<LibraryUser> _userManager;

        public RegisterUseCase(IUnitOfWork unitOfWork,
            LibraryUserValidator libraryUserValidator,
            UserManager<LibraryUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _libraryUserValidator = libraryUserValidator;
            _userManager = userManager;
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
            
            if (await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == libraryUser.UserName.ToLower()) != null)
            {
                throw new LoginAlreadyExistsException($"Login {libraryUser.UserName} is already in use!");
            }

            var resultUser = await _userManager.CreateAsync(libraryUser, registerDto.Password);

            if (!resultUser.Succeeded)
            {
                throw new UserCreationException($"User {libraryUser.UserName} was not created");
            }

            var resultClaim = await _userManager.AddClaimAsync(libraryUser, new Claim(ClaimTypes.Role, "User"));

            if (!resultClaim.Succeeded)
            {
                throw new AddClaimException($"User {libraryUser.UserName} was not assigned a claim");
            }

            await _unitOfWork.SaveChangesAsync();

            return libraryUser;
        }
    }
}
