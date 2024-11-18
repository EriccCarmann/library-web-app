using Library.Application.DTOs.LibraryUserDTOs;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq;

namespace Library.Application.UseCases.AccountUseCases
{
    public class LoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenerateToken _generateToken;
        private readonly IGenerateRefreshToken _generateRefreshToken;

        public LoginUseCase(
            IUnitOfWork unitOfWork, 
            IGenerateToken generateToken, 
            IGenerateRefreshToken generateRefreshToken)
        {
            _unitOfWork = unitOfWork;
            _generateToken = generateToken;
            _generateRefreshToken = generateRefreshToken;
        }

        public async Task<ShowLoggedInUserDto> Login(LoginDto loginDto)
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
