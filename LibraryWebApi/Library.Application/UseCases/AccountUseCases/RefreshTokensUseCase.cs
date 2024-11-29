using Library.Application.DTOs.LibraryUserDTOs;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.UseCases.AccountUseCases
{
    public class RefreshTokensUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenerateToken _generateToken;
        private readonly IGenerateRefreshToken _generateRefreshToken;
        private readonly UserManager<LibraryUser> _userManager;

        public RefreshTokensUseCase(
            IUnitOfWork unitOfWork, 
            IGenerateToken generateToken, 
            IGenerateRefreshToken generateRefreshToken,
            UserManager<LibraryUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _generateToken = generateToken;
            _generateRefreshToken = generateRefreshToken;
            _userManager = userManager;
        }

        public async Task<ShowLoggedInUserDto> RefreshTokens(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDto.UserName.ToLower());

            if (user == null)
            {
                throw new EntityNotFoundException($"User {loginDto.UserName} is not found in database.");
            }

            if (user.TokenExpires < DateTime.Now)
            {
                throw new TokenExpiredException("Token is expired");
            }

            var token = _generateToken.CreateToken(user);

            _unitOfWork.SaveChangesAsync();

            return new ShowLoggedInUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = token.Result,
                RefreshToken = user.RefreshToken
            };
        }
    }
}
