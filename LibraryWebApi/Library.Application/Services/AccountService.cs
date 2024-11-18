using Library.Domain.Entities;
using Library.Domain.Helpers;
using Library.Application.DTOs.LibraryUserDTOs;
using Microsoft.AspNetCore.Mvc;
using Library.Application.UseCases.AccountUseCases;

namespace LibraryWebApi.Services
{
    public class AccountService
    {
        private readonly GetAllUsersUseCase _getAllUsersUseCase;
        private readonly RegisterUseCase _registerUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly LogoutUseCase _logoutUseCase;
        private readonly RefreshTokensUseCase _refreshTokensUseCase;

        public AccountService(
            GetAllUsersUseCase getAllUsersUseCase,
            RegisterUseCase registerUseCase,
            LoginUseCase loginUseCase,
            LogoutUseCase logoutUseCase,
            RefreshTokensUseCase refreshTokensUseCase)
        {
            _getAllUsersUseCase = getAllUsersUseCase;
            _registerUseCase = registerUseCase;
            _loginUseCase = loginUseCase;
            _logoutUseCase = logoutUseCase;
            _refreshTokensUseCase = refreshTokensUseCase;
        }

        public async Task<IEnumerable<LibraryUser>> GetAll([FromQuery] QueryObject queryObject)
        {
            return await _getAllUsersUseCase.GetAll(queryObject);
        }

        public async Task<LibraryUser> Register([FromBody] RegisterDto registerDto)
        {
            return await _registerUseCase.Register(registerDto);
        }

        public async Task<ShowLoggedInUserDto> Login(LoginDto loginDto)
        {
            return await _loginUseCase.Login(loginDto);
        }

        public async Task<ShowLoggedInUserDto> Refresh(LoginDto loginDto)
        {
            return await _refreshTokensUseCase.RefreshTokens(loginDto);
        }

        public async Task Logout()
        {
            await _logoutUseCase.Logout();
        }
    }
}
