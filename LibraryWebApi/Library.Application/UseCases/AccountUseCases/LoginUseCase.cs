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

        public LoginUseCase(
            IUnitOfWork unitOfWork, 
            IGenerateToken generateToken)
        {
            _unitOfWork = unitOfWork;
            _generateToken = generateToken;
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

            return new Tuple<ShowNewUserDto, string>(new ShowNewUserDto
            {
                UserName = user.UserName,
                Email = user.Email
            },
            token);
        }
    }
}
