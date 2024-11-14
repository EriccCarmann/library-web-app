using FluentAssertions;
using FakeItEasy;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using LibraryWebApi.Controllers;
using Library.Application.Services;
using Library.Application.DTOs.BookDTOs;

namespace LibraryWebApi.Tests.ControllersTests
{
    public class BookControllerTests
    {
        private readonly BookService _bookService;
        private readonly UserManager<LibraryUser> _userManager;

        private readonly BookController _bookController;

        public BookControllerTests()
        {
            _bookService = A.Fake<BookService>();

            _bookController = new BookController(_userManager, _bookService);
        }

        [Fact]
        public void BookController_GetById_ReturnBook()
        {
            Book data = new Book()
            {
                Id = 1
            };

            var result = _bookController.GetById(1);

            result.Should().NotBeNull();
        }

        [Fact]
        public void BookController_CreateBook_ReturnBook()
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

        [Fact]
        public void BookController_UpdateBook_ReturnBook()
        {
            Book data = new Book()
            {
                Id = 1,
                Title = "Title",
                Genre = "Genre",
                Description = "Description",
                ISBN = "1234567891011"
            };

            BookUpdateDto dataDto = new BookUpdateDto()
            {
                Title = "NewTitle",
                Genre = "NewGenre",
                Description = "NewDescription",
                ISBN = "2234567891011"
            };

            var result = _bookController.UpdateBook(data.Id, dataDto);

            result.Should().NotBeNull();
        }
    }
}
