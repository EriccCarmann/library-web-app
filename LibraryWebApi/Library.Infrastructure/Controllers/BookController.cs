using AutoMapper;
using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Validators;
using Library.Domain.Entities.BookDTOs;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            var books = await _bookRepository.GetAllAsync();
            var _books = _mapper.Map<IEnumerable<Book>>(books);

            return Ok(_books);
        }

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
