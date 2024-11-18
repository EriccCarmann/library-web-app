using Library.Application.DTOs.LibraryUserDTOs;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.AccountUseCases
{
    public class RefreshTokensUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenerateToken _generateToken;
        private readonly IGenerateRefreshToken _generateRefreshToken;

        public RefreshTokensUseCase(
            IUnitOfWork unitOfWork, 
            IGenerateToken generateToken, 
            IGenerateRefreshToken generateRefreshToken)
        {
            _unitOfWork = unitOfWork;
            _generateToken = generateToken;
            _generateRefreshToken = generateRefreshToken;
        }

        public async Task<ShowLoggedInUserDto> RefreshTokens(LoginDto loginDto)
        {
            var user = _unitOfWork.Account.FindUserByName(loginDto.UserName).Result;
            var refreshToken = user.RefreshToken;

            if (user.TokenExpires < DateTime.Now)
            {
                throw new EntityNotFoundException($"Token expired");
            }

            var token = _generateToken.CreateToken(user);

            var newRefreshToken = _generateRefreshToken.CreateRefreshToken();

            user.RefreshToken = newRefreshToken.Token;
            user.TokenExpires = newRefreshToken.Expires;

            _unitOfWork.SaveChangesAsync();

            return new ShowLoggedInUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = token.Result,
                RefreshToken = newRefreshToken.Token
            };
        }
    }
}
