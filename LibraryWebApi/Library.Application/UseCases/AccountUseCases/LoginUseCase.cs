using Library.Application.DTOs.LibraryUserDTOs;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.AccountUseCases
{
    public class LoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
