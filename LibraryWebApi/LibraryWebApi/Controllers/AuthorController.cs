using AutoMapper;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Helpers;
using Library.Application.DTOs.AuthorDTOs;
using LibraryWebApi.Services;

namespace LibrLibraryWebApiary.Controllers
{
    [Route("/author")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorController(
            IMapper mapper,
            AuthorService authorService) 
        {
            _authorService = authorService;
        }

        [Authorize(Policy = "User")]
        [HttpGet("getall")]
        public async Task<ActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var authors = await _authorService.GetAll(queryObject);

            return Ok(authors);
        }

        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult?> GetById([FromRoute] int id)
        {
            var author = await _authorService.GetById(id);

            return Ok(author);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("createauthor")]
        public async Task<IActionResult> CreateAuthor(AuthorCreateDto author) 
        {
            var result = await _authorService.CreateAuthor(author);

            return CreatedAtAction("CreateAuthor", result);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("updateauthor")]
        public async Task<IActionResult?> UpdateAuthor(string name, [FromBody] AuthorUpdateDto authorUpdateDto) 
        {
            var author = await _authorService.UpdateAuthor(name, authorUpdateDto);

            return Ok(author);
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult?> DeleteAuthor([FromRoute] int id)
        {
            await _authorService.DeleteAuthor(id);

            return NoContent();
        }

        [Authorize(Policy = "User")]
        [HttpGet("findauthorbyname")]
        public async Task<Author?> FindAuthorByName(string name)
        {
            return await _authorService.FindAuthorByName(name);
        }
    }
}