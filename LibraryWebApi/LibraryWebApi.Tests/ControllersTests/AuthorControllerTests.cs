using AutoMapper;
using FakeItEasy;
using Library.Domain.Entities;
using FluentAssertions;
using LibraryWebApi.Controllers;
using LibraryWebApi.DTOs.AuthorDTOs;
using LibraryWebApi.Services;
using Library.Application.Validators;

namespace LibraryWebApi.Tests.ControllersTests
{
    public class AuthorControllerTests
    {
        private readonly IMapper _mapper;
        private readonly AuthorService _authorService;
        private readonly AuthorValidator _authorValidator;

        private readonly AuthorController _authorController;

        public AuthorControllerTests()
        {
            _mapper = A.Fake<IMapper>();
            _authorService = A.Fake<AuthorService>();

            _authorController = new AuthorController(_mapper, _authorService);
        }

        [Fact]
        public void AuthorController_GetById_ReturnAuthor()
        {
            Author author = new Author()
            {
                Id = 1
            };

            var result = _authorController.GetById(1);

            result.Should().NotBeNull();
        }

        [Fact]
        public void AuthorController_CreateAuthor_ReturnAuthor()
        {
            AuthorCreateDto author = new AuthorCreateDto()
            {
                FirstName = "Test",
                LastName = "Test",
                DateOfBirth = new DateOnly(1960, 11, 10),
                Country = "Test"
            };

            var result = _authorController.CreateAuthor(author);

            result.Should().NotBeNull();
        }
        
        [Fact]
        public void AuthorController_UpdateAuthor_ReturnAuthor()
        {
            Author author = new Author()
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test",
                DateOfBirth = new DateOnly(1960, 11, 10),
                Country = "Test"
            };

            AuthorUpdateDto authorUpdate = new AuthorUpdateDto()
            {
                FirstName = "NewTest",
                LastName = "NewTest",
                DateOfBirth = new DateOnly(1960, 11, 10),
                Country = "NewTest"
            };

            var result = _authorController.UpdateAuthor(author.Id.ToString(), authorUpdate);

        }
    }
}
