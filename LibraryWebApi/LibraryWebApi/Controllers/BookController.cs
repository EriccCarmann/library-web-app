using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Helpers;
using Microsoft.AspNetCore.Identity;
using LibraryWebApi.DTOs.BookDTOs;
using LibraryWebApi.Services;

namespace LibraryWebApi.Controllers
{
    [Route("/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly UserManager<LibraryUser> _userManager;
        private readonly BookService _bookService;

        public BookController(
            UserManager<LibraryUser> userManager,
            BookService bookService)
        {
            _userManager = userManager;
            _bookService = bookService;
        }
     
        [Authorize(Policy = "User")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var books = await _bookService.GetAll(queryObject);

            return Ok(books);
        }

        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var book = await _bookService.GetById(id);

            return Ok(book);
        }

        [Authorize(Policy = "User")]
        [HttpGet("getbyISBN")]
        public async Task<IActionResult> GetByISBN(string ISBN)
        {
            var book = await _bookService.GetByISBN(ISBN);

            return Ok(book);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("{id}")]
         public async Task<IActionResult> CreateBook(BookCreateDto data) 
         {
            var _newBook = await _bookService.CreateBook(data);

            return CreatedAtAction("CreateBook", _newBook);
         }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookUpdateDto bookUpdatingDto) 
        {
            var updatedBook = await _bookService.UpdateBook(id, bookUpdatingDto);

            return Ok(updatedBook);
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id) 
        {
            await _bookService.DeleteBook(id);

            return NoContent();
        }

        [Authorize(Policy = "User")]
        [HttpPut("takebook")]
        public async Task<IActionResult> TakeBook(string bookName)
        {
            var takeBook = await _bookService.TakeBook(bookName, 
                _userManager.GetUserId(User).ToString());

            return Ok(takeBook);
        }

        [Authorize(Policy = "User")]
        [HttpPut("gettakenbooks")]
        public async Task<IActionResult> GetTakenBooks([FromQuery] QueryObject query)
        {
            var takenBooks = await _bookService.GetTakenBooks(query,
                _userManager.GetUserId(User).ToString());

            return Ok(takenBooks);
        }

        [Authorize(Policy = "User")]
        [HttpPut("returntakenbook")]
        public async Task<IActionResult> ReturnBook(string bookName)
        {
            var book = await _bookService.ReturnBook(bookName, 
                _userManager.GetUserId(User).ToString());

            return Ok(book);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("addcover")]
        public async Task<IActionResult> AddCover(string bookTitle, IFormFile file, [FromQuery] QueryObject queryObject)
        {
            var result = _bookService.AddCover(bookTitle, file, queryObject);

            return Ok(result);
        }
    }
}
