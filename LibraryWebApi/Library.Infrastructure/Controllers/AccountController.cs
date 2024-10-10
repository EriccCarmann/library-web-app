﻿using IdentityServer4.Services;
using Library.Domain.Entities;
using Library.Domain.Entities.LibraryUserDTOs;
using Library.Domain.Interfaces;
using Library.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Infrastructure.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<LibraryUser> _userManager;
        private readonly SignInManager<LibraryUser> _signInManager;
        private readonly IIdentityServerInteractionService _interactionService;

        private readonly ApplicationDBContext _context;
        
        public AccountController(UserManager<LibraryUser> userManager, 
            SignInManager<LibraryUser> signInManager, 
            ApplicationDBContext context,
            IIdentityServerInteractionService interactionService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _interactionService = interactionService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) 
        {
            try 
            {
                if (!ModelState.IsValid) 
                {
                    return BadRequest(ModelState);
                }

                var libraryUser = new LibraryUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

                var createUser = await _userManager.CreateAsync(libraryUser, registerDto.Password); // like this
                //var createUser = _userManager.CreateAsync(libraryUser, registerDto.Password).GetAwaiter().GetResult(); // or like that

                if (createUser.Succeeded)
                {
                    //var roleResult = await _userManager.AddToRoleAsync(libraryUser, "User");
                    var roleResult = await _userManager.AddClaimAsync(libraryUser, new Claim(ClaimTypes.Role, "User"));

                    if (roleResult.Succeeded)
                    {
                        return Ok(libraryUser);
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else 
                {
                    return StatusCode(500, createUser.Errors);
                }
            } 
            catch (Exception ex) 
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto, SignInManager<LibraryUser> signInManager) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null) return Unauthorized("Invalid Username");

            //var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);


            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new ShowNewUserDto 
                {
                    UserName = user.UserName,
                    Email = user.Email
                }
            );
        }

        [Authorize(Policy = "User")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            return Ok(users);
        }
    }
}