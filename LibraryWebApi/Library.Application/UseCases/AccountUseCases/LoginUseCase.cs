using Library.Application.DTOs.LibraryUserDTOs;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Library.Application.UseCases.AccountUseCases
{
    public class LoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenerateToken _generateToken;
        private readonly IGenerateRefreshToken _generateRefreshToken;

        private readonly IHttpContextAccessor _context;

        public LoginUseCase(
            IUnitOfWork unitOfWork, 
            IGenerateToken generateToken, 
            IGenerateRefreshToken generateRefreshToken,
            IHttpContextAccessor context)
        {
            _unitOfWork = unitOfWork;
            _generateToken = generateToken;
            _generateRefreshToken = generateRefreshToken;
            _context = context;
        }

        public async Task<Tuple<ShowNewUserDto, string>> Login(LoginDto loginDto)
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

            _context.HttpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, 
                new CookieOptions
                {
                    HttpOnly = true,
                    Expires = refreshToken.Expires
                });

            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;

            await _unitOfWork.SaveChangesAsync();

            return new Tuple<ShowNewUserDto, string>(new ShowNewUserDto
            {
                UserName = user.UserName,
                Email = user.Email
            },
            token);
        }
    }
}
