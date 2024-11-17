using Library.Domain.Entities;
using Library.Domain.Helpers;
using Library.Application.DTOs.LibraryUserDTOs;
using Microsoft.AspNetCore.Mvc;
using Library.Application.UseCases.AccountUseCases;
using Microsoft.AspNetCore.Http;

namespace LibraryWebApi.Services
{
    public class AccountService
    {
        private readonly GetAllUsersUseCase _getAllUsersUseCase;
        private readonly RegisterUseCase _registerUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly LogoutUseCase _logoutUseCase;

        public AccountService(
            GetAllUsersUseCase getAllUsersUseCase,
            RegisterUseCase registerUseCase,
            LoginUseCase loginUseCase,
            LogoutUseCase logoutUseCase)
        {
            _getAllUsersUseCase = getAllUsersUseCase;
            _registerUseCase = registerUseCase;
            _loginUseCase = loginUseCase;
            _logoutUseCase = logoutUseCase;
        }

        public async Task<IEnumerable<LibraryUser>> GetAll([FromQuery] QueryObject queryObject)
        {
            return await _getAllUsersUseCase.GetAll(queryObject);
        }

        public async Task<LibraryUser> Register([FromBody] RegisterDto registerDto)
        {
            return await _registerUseCase.Register(registerDto);
        }

        public async Task<Tuple<ShowNewUserDto, string>> Login(LoginDto loginDto)
        {
            return await _loginUseCase.Login(loginDto);
        }

        public async Task Logout()
        {
            await _logoutUseCase.Logout();
        }
    }
}
