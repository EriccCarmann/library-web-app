using Library.Domain.Interfaces;
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

        [HttpGet("ä")]
        //[Authorize]
        public async Task<IActionResult> GetBooksAuthors()
        {
            var all = await _authorBookRepository.GetAllAsync();

            return Ok(all);
        }

        [HttpGet("b")]
        public async Task<IActionResult> GetBooksByAuthors(string name)
        {
            var all = await _authorBookRepository.GetBooksByAuthorAsync(name);

            return Ok(all);
        }
        /*  [HttpPut("createBooksAuthors")]
          public async Task<IActionResult> CreateBooksAuthors()
          {

          }*/
    }
}
