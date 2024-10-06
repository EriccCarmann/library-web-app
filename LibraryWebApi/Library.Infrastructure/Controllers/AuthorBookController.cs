using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Infrastructure.Controllers
{
    [Route("api/authorBook")]
    [ApiController]
    public class AuthorBookController : ControllerBase
    {
        public IAuthorRepository _authorRepository;
        public IBookRepository _bookRepository;

        public AuthorBookController(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }
    }
}
