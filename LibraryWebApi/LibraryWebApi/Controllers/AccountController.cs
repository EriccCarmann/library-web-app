using Library.Domain.Helpers;
using LibraryWebApi.DTOs.LibraryUserDTOs;
using LibraryWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            return Ok(await _accountService.GetAll(queryObject));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) 
        {
            var user = await _accountService.Register(registerDto);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto) 
        {
            var user = await _accountService.Login(loginDto);

            return Ok(user);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return Ok();
        }
    }
}
