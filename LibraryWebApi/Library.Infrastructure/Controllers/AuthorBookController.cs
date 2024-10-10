/*using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Infrastructure.Controllers
{
    [Route("api/authorbook")]
    [ApiController]
    public class AuthorBookController : ControllerBase
    {
        public IAuthorBookRepository _authorBookRepository;
        public IAuthorRepository _authorRepository;
        public IBookRepository _bookRepository;
        
        public AuthorBookController(IAuthorBookRepository authorBookRepository, IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorBookRepository = authorBookRepository;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet("GetAll")]
        //[Authorize]
        public async Task<IActionResult> GetBooksAuthors()
        {
            var all = await _authorBookRepository.GetAllAsync();

            return Ok(all);
        }

        [HttpGet("GetByAuthors")]
        public async Task<IActionResult> GetBooksByAuthors(string name)
        {
            var all = await _authorBookRepository.GetBooksByAuthorAsync(name);

            return Ok(all);
        }

        [HttpPost("CreateBooksAuthors")]
        public async Task<IActionResult> CreateBooksAuthors(string authorName, string bookName)
        {
            var authorBook = await _authorBookRepository.CreateBooksAuthorsAsync(authorName, bookName);

            if (authorBook == null) 
            {
                return StatusCode(500, "Could not create");
            }
            else 
            {
                return Ok("You've connected author and book");
            }
        }
    }
}
*/