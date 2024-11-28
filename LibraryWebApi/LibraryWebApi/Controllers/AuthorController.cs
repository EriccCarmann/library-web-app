using AutoMapper;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Helpers;
using Library.Application.DTOs.AuthorDTOs;
using LibraryWebApi.Services;
using Library.Application.UseCases.AuthorUseCases;

namespace LibrLibraryWebApiary.Controllers
{
    [Route("/author")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly GetAllAuthorsUseCase _getAllAuthorsUseCase;
        private readonly GetAuthorByIdUseCase _getAuthorByIdUseCase;
        private readonly CreateAuthorUseCase _createAuthorUseCase;
        private readonly UpdateAuthorUseCase _updateAuthorUseCase;
        private readonly DeleteAuthorUseCase _deleteAuthorUseCase;
        private readonly FindAuthorByNameUseCase _findAuthorByNameUseCase;

        public AuthorController(
            IMapper mapper,
            GetAllAuthorsUseCase getAllAuthorsUseCase,
            GetAuthorByIdUseCase getAuthorByIdUseCase,
            CreateAuthorUseCase createAuthorUseCase,
            UpdateAuthorUseCase updateAuthorUseCase,
            DeleteAuthorUseCase deleteAuthorUseCase,
            FindAuthorByNameUseCase findAuthorByNameUseCase) 
        {
            _getAllAuthorsUseCase = getAllAuthorsUseCase;
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
            _createAuthorUseCase = createAuthorUseCase;
            _updateAuthorUseCase = updateAuthorUseCase;
            _deleteAuthorUseCase = deleteAuthorUseCase;
            _findAuthorByNameUseCase = findAuthorByNameUseCase;
        }

        [Authorize(Policy = "User")]
        [HttpGet("getall")]
        public async Task<ActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var authors = await _getAllAuthorsUseCase.GetAll(queryObject);

            return Ok(authors);
        }

        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult?> GetById([FromRoute] int id)
        {
            var author = await _getAuthorByIdUseCase.GetById(id);

            return Ok(author);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("createauthor")]
        public async Task<IActionResult> CreateAuthor(AuthorCreateDto author) 
        {
            var result = await _createAuthorUseCase.CreateAuthor(author);

            return CreatedAtAction("CreateAuthor", result);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("updateauthor")]
        public async Task<IActionResult?> UpdateAuthor(string name, [FromBody] AuthorUpdateDto authorUpdateDto) 
        {
            var author = await _updateAuthorUseCase.UpdateAuthor(name, authorUpdateDto);

            return Ok(author);
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult?> DeleteAuthor([FromRoute] int id)
        {
            await _deleteAuthorUseCase.DeleteAuthor(id);

            return NoContent();
        }

        [Authorize(Policy = "User")]
        [HttpGet("findauthorbyname")]
        public async Task<Author?> FindAuthorByName(string name)
        {
            return await _findAuthorByNameUseCase.FindAuthorByName(name);
        }
    }
}