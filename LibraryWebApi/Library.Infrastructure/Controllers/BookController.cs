using AutoMapper;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Validators;
using Library.Domain.Entities.BookDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Helpers;

namespace Library.Infrastructure.Controllers
{
    [Route("/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly BookValidator _bookValidator;

        public BookController(IMapper mapper, IBookRepository bookRepository, BookValidator bookValidator)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _bookValidator = bookValidator;
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public async Task<ActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var books = await _bookRepository.GetAllAsync(queryObject);
            var _books = _mapper.Map<IEnumerable<Book>>(books);

            return Ok(_books);
        }

        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null) 
            { 
                return NotFound();
            }

            return Ok(book);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
         public async Task<IActionResult> CreateBook(BookCreateDto data) 
         {
             if (ModelState.IsValid)
             {
                var _book = _mapper.Map<Book>(data);

                if (!_bookValidator.Validate(_book).IsValid)
                {
                    return BadRequest();
                }

                await _bookRepository.CreateAsync(_book);

                var _newBook = _mapper.Map<BookReadDto>(_book);

                return CreatedAtAction("CreateBook", new {_book.Id}, _newBook);
             }
             return BadRequest();
         }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookUpdateDto bookUpdatingDto) 
        {
            var updateBook = await _bookRepository.UpdateAsync(id, bookUpdatingDto);

            if (updateBook == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BookReadDto>(updateBook));
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id) 
        {
            var existingBook = await _bookRepository.DeleteAsync(id);

            if (existingBook == null)
                return NotFound();

            return NoContent();
        }
    }
}
