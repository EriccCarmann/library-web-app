using AutoMapper;
using FluentAssertions;
using Library.Domain.Entities.BookDTOs;
using Library.Domain.Interfaces;
using Library.Domain.Validators;
using Library.Infrastructure.Controllers;
using FakeItEasy;

namespace LibraryWebApi.Tests.ControllersTests
{
    public class BookControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly BookValidator _bookValidator;

        private readonly BookController _bookController;

        public BookControllerTests()
        {
            _mapper = A.Fake<IMapper>();
            _bookValidator = A.Fake<BookValidator>();
            _bookRepository = A.Fake<IBookRepository>();

            _bookController = new BookController(_mapper, _bookRepository, _bookValidator);
        }

        [Fact]
        public void BookController_CreateBook_ReturnIActionResult()
        {
            BookCreateDto data = new BookCreateDto()
            {
                Title = "Title",
                Genre = "Genre",
                Description = "Description",
                ISBN = "1234567891011"
            };

            var result = _bookController.CreateBook(data);

            result.Should().NotBeNull();
        }
    }
}
