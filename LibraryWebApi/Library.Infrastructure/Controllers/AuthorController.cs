using AutoMapper;
using Library.Domain.Interfaces;
using Library.Domain.Entities.AuthorDTOs;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Helpers;

namespace Library.Infrastructure.Controllers
{
    [Route("/author")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IMapper mapper, IAuthorRepository authorRepository) 
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        [Authorize(Policy = "User")]
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var authors = await _authorRepository.GetAllAsync(queryObject);

            var _authors = _mapper.Map<IEnumerable<AuthorReadDto>>(authors);

            return Ok(_authors);
        }

        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorCreateDto author) 
        {
            if (ModelState.IsValid) 
            {
                var _author = _mapper.Map<Author>(author);

                await _authorRepository.CreateAsync(_author);

                var newAuthor = _mapper.Map<AuthorReadDto>(_author);

                return CreatedAtAction("CreateAuthor", new { _author.Id}, author);
            }
            return BadRequest();
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor([FromRoute] int id, [FromBody] AuthorUpdateDto authorUpdateDto) 
        {
            var updateAuthor = await _authorRepository.UpdateAsync(id, authorUpdateDto);

            if (updateAuthor == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorReadDto>(updateAuthor));
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            var existingAuthor = await _authorRepository.DeleteAsync(id);

            if (existingAuthor == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
