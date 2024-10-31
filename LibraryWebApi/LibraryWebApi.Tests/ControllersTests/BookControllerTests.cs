using AutoMapper;
using FluentAssertions;
using Library.Domain.Interfaces;
using FakeItEasy;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using LibraryWebApi.Validators;
using LibraryWebApi.Controllers;
using LibraryWebApi.DTOs.BookDTOs;
using Library.Infrastructure.Repository.UnitOfWork;

namespace LibraryWebApi.Tests.ControllersTests
{
    public class BookControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IGenericRepository<Book> _genericRepository;
        private readonly BookValidator _bookValidator;
        private readonly UserManager<LibraryUser> _userManager;

        private readonly BookController _bookController;

        public BookControllerTests()
        {
            _mapper = A.Fake<IMapper>();
            _bookValidator = A.Fake<BookValidator>();
            _bookRepository = A.Fake<IBookRepository>();
            _genericRepository = A.Fake<IGenericRepository<Book>>();
            _userManager = A.Fake<UserManager<LibraryUser>>();

            _bookController = new BookController(_mapper, _bookRepository, _genericRepository, _bookValidator, _userManager);
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
