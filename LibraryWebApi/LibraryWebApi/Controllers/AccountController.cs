using Library.Domain.Helpers;
using Library.Application.DTOs.LibraryUserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Application.UseCases.AccountUseCases;

namespace LibraryWebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly GetAllUsersUseCase _getAllUsersUseCase;
        private readonly RegisterUseCase _registerUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly LogoutUseCase _logoutUseCase;
        private readonly RefreshTokensUseCase _refreshTokensUseCase;

        public AccountController(
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

        [Authorize(Policy = "Admin")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            return Ok(await _getAllUsersUseCase.GetAll(queryObject));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) 
        {
            return Ok(await _registerUseCase.Register(registerDto));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto) 
        {
            return Ok(await _loginUseCase.Login(loginDto));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(LoginDto loginDto)
        {
            return Ok(await _refreshTokensUseCase.RefreshTokens(loginDto));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _logoutUseCase.Logout();
            return Ok();
        }
    }
}
