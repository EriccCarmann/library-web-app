using Library.Domain.Entities;
using Library.Domain.Helpers;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.UnitOfWork;
using LibraryWebApi.DTOs.LibraryUserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IGenericRepository<LibraryUser> _genericRepository;

        public AccountController(
            IAccountRepository accountRepository,
            IGenericRepository<LibraryUser> genericRepository)
        {
            _accountRepository = accountRepository;
            _genericRepository = genericRepository;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            return Ok(await _genericRepository.GetAllAsync(queryObject));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var libraryUser = new LibraryUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };

            return Ok(await _accountRepository.Register(libraryUser, registerDto.Password));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            var user = await _accountRepository.Login(loginDto.UserName, loginDto.Password);

            return Ok(
                new ShowNewUserDto 
                {
                    UserName = user.UserName,
                    Email = user.Email
                }
            );
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.Logout();
            return Ok();
        }
    }
}
