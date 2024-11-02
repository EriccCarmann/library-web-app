using Library.Domain.Entities;
using Library.Domain.Helpers;
using Library.Infrastructure.UnitOfWork;
using LibraryWebApi.DTOs.LibraryUserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            return Ok(await _unitOfWork.Account.GetAllAsync(queryObject));
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

            var user = await _unitOfWork.Account.Register(libraryUser, registerDto.Password);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            var user = await _unitOfWork.Account.Login(loginDto.UserName, loginDto.Password);

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
            await _unitOfWork.Account.Logout();
            return Ok();
        }
    }
}
