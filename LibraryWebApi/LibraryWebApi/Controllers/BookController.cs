using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.Helpers;
using Microsoft.AspNetCore.Identity;
using Library.Application.DTOs.BookDTOs;
using Library.Application.UseCases.BookUseCases;

namespace LibraryWebApi.Controllers
{
    [Route("/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly UserManager<LibraryUser> _userManager;
        private readonly GetAllBooksUseCase _getAllBooksUseCase;
        private readonly GetBookByIdUseCase _getBookByIdUseCase;
        private readonly GetBookByISBNUseCase _getBookByISBNUseCase;
        private readonly CreateBookUseCase _createBookUseCase;
        private readonly UpdateBookUseCase _updateBookUseCase;
        private readonly DeleteBookUseCase _deleteBookUseCase;
        private readonly TakeBookUseCase _takeBookUseCase;
        private readonly GetTakenBooksUseCase _getTakenBooksUseCase;
        private readonly ReturnBookUseCase _returnBookUseCase;
        private readonly AddCoverUseCase _addCoverUseCase;

        public BookController(
            UserManager<LibraryUser> userManager,
            GetAllBooksUseCase getAllBooksUseCase,
            GetBookByIdUseCase getBookByIdUseCase,
            GetBookByISBNUseCase getBookByISBNUseCase,
            CreateBookUseCase createBookUseCase,
            UpdateBookUseCase updateBookUseCase,
            DeleteBookUseCase deleteBookUseCase,
            TakeBookUseCase takeBookUseCase,
            GetTakenBooksUseCase getTakenBooksUseCase,
            ReturnBookUseCase returnBookUseCase,
            AddCoverUseCase addCoverUseCase)
        {
            _userManager = userManager;
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
            _getBookByISBNUseCase = getBookByISBNUseCase;
            _createBookUseCase = createBookUseCase;
            _updateBookUseCase = updateBookUseCase;
            _deleteBookUseCase = deleteBookUseCase;
            _takeBookUseCase = takeBookUseCase;
            _getTakenBooksUseCase = getTakenBooksUseCase;
            _returnBookUseCase = returnBookUseCase;
            _addCoverUseCase = addCoverUseCase;
        }

        [Authorize(Policy = "User")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var books = await _getAllBooksUseCase.GetAll(queryObject);

            return Ok(books);
        }

        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var book = await _getBookByIdUseCase.GetById(id);

            return Ok(book);
        }

        [Authorize(Policy = "User")]
        [HttpGet("getbyISBN")]
        public async Task<IActionResult> GetByISBN(string ISBN)
        {
            var book = await _getBookByISBNUseCase.GetByISBN(ISBN);

            return Ok(book);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("createbook")]
         public async Task<IActionResult> CreateBook(BookCreateDto data) 
         {
            var _newBook = await _createBookUseCase.CreateBook(data);

            return CreatedAtAction("CreateBook", _newBook);
         }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookUpdateDto bookUpdatingDto) 
        {
            var updatedBook = await _updateBookUseCase.UpdateBook(id, bookUpdatingDto);

            return Ok(updatedBook);
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id) 
        {
            await _deleteBookUseCase.DeleteBook(id);

            return NoContent();
        }

        [Authorize(Policy = "User")]
        [HttpPut("takebook")]
        public async Task<IActionResult> TakeBook(string bookName)
        {
            Console.WriteLine("dsfkgljsl;tjslkjlkdjfghlsdkjhl   dfklsghjd;fhk's't" + _userManager.GetUserId(User).ToString());

            var takeBook = await _takeBookUseCase.TakeBook(bookName, _userManager.GetUserId(User).ToString());

            return Ok(takeBook);
        }

        [Authorize(Policy = "User")]
        [HttpPut("gettakenbooks")]
        public async Task<IActionResult> GetTakenBooks([FromQuery] QueryObject query)
        {
            var takenBooks = await _getTakenBooksUseCase.GetTakenBooks(query, _userManager.GetUserId(User).ToString());

            return Ok(takenBooks);
        }

        [Authorize(Policy = "User")]
        [HttpPut("returntakenbook")]
        public async Task<IActionResult> ReturnBook(string bookName)
        {
            var book = await _returnBookUseCase.ReturnBook(_userManager.GetUserId(User).ToString(), bookName);

            return Ok(book);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("addcover")]
        public async Task<IActionResult> AddCover(string bookTitle, IFormFile file, [FromQuery] QueryObject queryObject)
        {
            var result = await _addCoverUseCase.AddCover(bookTitle, file, queryObject);

            return Ok(result);
        }
    }
}
