using AutoMapper;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Helpers;
using LibraryWebApi.Validators;
using LibraryWebApi.DTOs.AuthorDTOs;
using Library.Domain.Interfaces.UnitOfWork;

namespace LibraryWebApi.Controllers
{
    [Route("/author")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenericRepository<Author> _genericRepository;
        private readonly AuthorValidator _authorValidator;

        public AuthorController(
            IMapper mapper, 
            IAuthorRepository authorRepository,
            IGenericRepository<Author> genericRepository,
            AuthorValidator authorValidato) 
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
            _genericRepository = genericRepository;
            _authorValidator = authorValidato;
        }

        [Authorize(Policy = "User")]
        [HttpGet("getall")]
        public async Task<ActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var authors = await _genericRepository.GetAllAsync(queryObject);

            var _authors = _mapper.Map<IEnumerable<AuthorReadDto>>(authors);

            return Ok(_authors);
        }

        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult?> GetById([FromRoute] int id)
        {
            var author = await _genericRepository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("createauthor")]
        public async Task<IActionResult> CreateAuthor(AuthorCreateDto author) 
        {
            if (ModelState.IsValid) 
            {
                var _author = _mapper.Map<Author>(author);

                if (!_authorValidator.Validate(_author).IsValid)
                {
                    return BadRequest();
                }

                await _genericRepository.CreateAsync(_author);

                var newAuthor = _mapper.Map<AuthorReadDto>(_author);

                return CreatedAtAction("CreateAuthor", new { _author.Id}, author);
            }
            return BadRequest();
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult?> UpdateAuthor([FromRoute] int id, [FromBody] AuthorUpdateDto authorUpdateDto) 
        {
            var updateAuthor = await _genericRepository.UpdateAsync(id, new Author
            {
                Id = id,
                FirstName = authorUpdateDto.FirstName,
                LastName = authorUpdateDto.LastName,
                DateOfBirth = authorUpdateDto.DateOfBirth,
                Country = authorUpdateDto.Country
            });

            return Ok(_mapper.Map<AuthorReadDto>(updateAuthor));
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult?> DeleteAuthor([FromRoute] int id)
        {
            var existingAuthor = await _genericRepository.DeleteAsync(id);

            if (existingAuthor == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(Policy = "User")]
        [HttpGet("findauthorbyname")]
        public async Task<Author?> FindAuthorByName(string name)
        {
            return await _authorRepository.FindAuthorByName(name);
        }
    }
}
