using AutoMapper;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Helpers;
using LibraryWebApi.Validators;
using LibraryWebApi.DTOs.AuthorDTOs;
using Library.Infrastructure.UnitOfWork;

namespace LibraryWebApi.Controllers
{
    [Route("/author")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AuthorValidator _authorValidator;

        public AuthorController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            AuthorValidator authorValidato) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _authorValidator = authorValidato;
        }

        [Authorize(Policy = "User")]
        [HttpGet("getall")]
        public async Task<ActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var authors = await _unitOfWork.Author.GetAllAsync(queryObject);

            var _authors = _mapper.Map<IEnumerable<AuthorReadDto>>(authors);

            return Ok(_authors);
        }

        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult?> GetById([FromRoute] int id)
        {
            var author = await _unitOfWork.Author.GetByIdAsync(id);

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

                await _unitOfWork.Author.CreateAsync(_author);

                var newAuthor = _mapper.Map<AuthorReadDto>(_author);

                return CreatedAtAction("CreateAuthor", new { _author.Id}, author);
            }
            return BadRequest();
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult?> UpdateAuthor([FromRoute] int id, [FromBody] AuthorUpdateDto authorUpdateDto) 
        {
            var updateAuthor = await _unitOfWork.Author.UpdateAsync(id, new Author
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
            var existingAuthor = await _unitOfWork.Author.DeleteAsync(id);

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
            return await _unitOfWork.Author.FindAuthorByName(name);
        }
    }
}