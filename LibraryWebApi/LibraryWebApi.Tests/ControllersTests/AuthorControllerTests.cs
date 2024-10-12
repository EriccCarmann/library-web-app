using AutoMapper;
using Library.Domain.Interfaces;
using Library.Domain.Validators;
using Library.Infrastructure.Controllers;
using FakeItEasy;
using Library.Domain.Entities;
using FluentAssertions;
using Library.Domain.Entities.AuthorDTOs;

namespace LibraryWebApi.Tests.ControllersTests
{
    public class AuthorControllerTests
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorValidator _authorValidator;

        private readonly AuthorController _authorController;

        public AuthorControllerTests()
        {
            _mapper = A.Fake<IMapper>();
            _authorRepository = A.Fake<IAuthorRepository>();
            _authorValidator = A.Fake<AuthorValidator>();

            _authorController = new AuthorController(_mapper, _authorRepository, _authorValidator);
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

            var result = _authorController.UpdateAuthor(author.Id, authorUpdate);

        }
    }
}
