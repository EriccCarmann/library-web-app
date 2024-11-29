using Library.Application.DTOs.LibraryUserDTOs;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.UseCases.AccountUseCases
{
    public class LoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenerateToken _generateToken;
        private readonly IGenerateRefreshToken _generateRefreshToken;
        private readonly UserManager<LibraryUser> _userManager;
        private readonly SignInManager<LibraryUser> _signInManager;

        public LoginUseCase(
            IUnitOfWork unitOfWork, 
            IGenerateToken generateToken, 
            IGenerateRefreshToken generateRefreshToken,
            UserManager<LibraryUser> userManager,
            SignInManager<LibraryUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _generateToken = generateToken;
            _generateRefreshToken = generateRefreshToken;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ShowLoggedInUserDto> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDto.UserName.ToLower());

            if (user == null)
            {
                throw new EntityNotFoundException($"User {loginDto.UserName} is not found in database.");
            }
            
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new InvalidPassworException();
            }
            
            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new WrongPasswordException();
            }

            string token = await _generateToken.CreateToken(user);

            var refreshToken = _generateRefreshToken.CreateRefreshToken();

            user.RefreshToken = refreshToken.Token;
            user.TokenExpires = refreshToken.Expires;

            await _unitOfWork.SaveChangesAsync();

            return new ShowLoggedInUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = token,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
